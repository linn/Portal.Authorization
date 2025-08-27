namespace Linn.Portal.Authorization.IoC.Logging.AmazonSQS.Models
{
    using System;

    using Linn.Common.Logging;

    using Logging;

    public class AmazonSqsLogModel
    {
        public DateTime Timestamp { get; set; }

        public string Sender { get; set; }

        public string Environment { get; set; }

        public int Level { get; set; }

        public AmazonSqsCallerInfo CallerInfo { get; set; }

        public LoggingProperty[] Properties { get; set; }

        public string Message { get; set; }

        public AmazonSqsLogExceptionModel Exception { get; set; }
    }
}