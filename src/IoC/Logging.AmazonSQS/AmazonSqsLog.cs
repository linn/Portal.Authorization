namespace Linn.Portal.Authorization.IoC.Logging.AmazonSQS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Amazon.SQS;

    using Linn.Common.Logging;
    using Linn.Common.Serialization.Json;

    using Logging;
    using Models;

    using Newtonsoft.Json;

    public class AmazonSqsLog : ILog
    {
        private readonly string environment;
        private readonly int maxInnerExceptionDepth;
        private readonly string queueUrl;

        private readonly string sender;

        private readonly IAmazonSQS client;

        private readonly AmazonSqsCallerAnalyser amazonSqsCallerAnalyser;

        private readonly object taskLock;
        private Task task;

        public AmazonSqsLog(
            IAmazonSQS client,
            string environment,
            int maxInnerExceptionDepth,
            string queueUrl)
            : this(client, environment, maxInnerExceptionDepth, queueUrl, Assembly.GetEntryAssembly().GetName().Name)
        {
        }

        public AmazonSqsLog(IAmazonSQS client, string environment, int maxInnerExceptionDepth, string queueUrl, string senderName)
        {
            this.client = client;
            this.environment = environment;
            this.maxInnerExceptionDepth = maxInnerExceptionDepth;
            this.queueUrl = queueUrl;

            this.sender = senderName;
            this.amazonSqsCallerAnalyser = new AmazonSqsCallerAnalyser(2);
            this.taskLock = new object();
        }

        public void Write(LoggingLevel level, IEnumerable<LoggingProperty> properties, string message, Exception ex = null)
        {
            var timestamp = DateTime.UtcNow;

            var callerInfo = this.amazonSqsCallerAnalyser.GetCallerInfo();

            var loggingProperties = properties.ToArray();

            lock (this.taskLock)
            {
                var previous = this.task ?? Task.CompletedTask;
                Task current = null;
                this.task = current = previous.ContinueWith(async t =>
                {
                    string serialized;

                    try
                    {
                        var model = new AmazonSqsLogModel
                        {
                            Timestamp = timestamp,
                            Sender = this.sender,
                            Environment = this.environment,
                            Level = (int)level,
                            CallerInfo = callerInfo,
                            Properties = loggingProperties,
                            Message = message,
                            Exception = AmazonSqsLogExceptionTransformer.Convert(this.maxInnerExceptionDepth, ex)
                        };

                        serialized = JsonConvert.SerializeObject(model, SerializerSettings.CamelCase);
                    }
                    catch (Exception serializationException)
                    {
                        serialized = this.CreateFallbackSerialization(level, loggingProperties, message, ex, timestamp, serializationException);
                    }

                    try
                    {
                        await this.client.SendMessageAsync(this.queueUrl, serialized);
                    }
                    finally
                    {
                        lock (this.taskLock)
                        {
                            // see #95
                            // if no further tasks have been enqueued, then we break the task continuation chain at the earliest opportunity to prevent leaks
                            // if we're always producing faster than we're consuming, then this will still leak
                            // but any producer/consumer solution would leak under such conditions unless messages were dropped
                            // this solution, although slightly inelegant, at least doesn't require this class to become a disposable or necessitate a long running thread or task
                            if (this.task == current)
                            {
                                this.task = null;
                            }
                        }
                    }
                });
            }
        }

        /* log failure to serialize original log message using 'easy to serialize' values only */

        private string CreateFallbackSerialization(LoggingLevel level, LoggingProperty[] properties, string message, Exception ex, DateTime timestamp, Exception serializationException)
        {
            AmazonSqsLogExceptionModel exception = null;

            try
            {
                exception = AmazonSqsLogExceptionTransformer.Convert(this.maxInnerExceptionDepth, serializationException);
            }
            catch
            {
                try
                {
                    exception = AmazonSqsLogExceptionTransformer.Convert(0, serializationException);
                }
                catch
                {
                    // give up on reporting the exception if unable to serialize it
                }
            }

            var fallbackProperties = this.CreateFallbackProperties(level, properties, message, ex);

            var model = new AmazonSqsLogModel
            {
                Timestamp = timestamp,
                Sender = this.sender,
                Environment = this.environment,
                Level = (int)LoggingLevel.Warning,
                Properties = fallbackProperties,
                Message = "Log serialization failure",
                Exception = exception
            };

            return JsonConvert.SerializeObject(model, SerializerSettings.CamelCase);
        }

        private LoggingProperty[] CreateFallbackProperties(LoggingLevel level, LoggingProperty[] properties, string message, Exception ex)
        {
            var fallbackProperties = new List<LoggingProperty>
            {
                new LoggingProperty
                {
                    Key = "originalLevel",
                    Value = (int)level
                },
                new LoggingProperty
                {
                    Key = "originalMessage",
                    Value = message
                }
            };

            if (properties.Any())
            {
                var propertyKeys = string.Join(", ", properties.Select(v => v.Key));

                fallbackProperties.Add(new LoggingProperty { Key = "originalProperties", Value = propertyKeys });
            }

            if (ex != null)
            {
                fallbackProperties.Add(new LoggingProperty { Key = "originalExceptionType", Value = ex.GetType().ToString() });

                if (!string.IsNullOrEmpty(ex.Message))
                {
                    fallbackProperties.Add(new LoggingProperty { Key = "originalExceptionMessage", Value = ex.Message });
                }
            }

            return fallbackProperties.ToArray();
        }
    }
}