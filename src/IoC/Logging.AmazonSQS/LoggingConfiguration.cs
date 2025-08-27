namespace Linn.Portal.Authorization.IoC.Logging.AmazonSQS
{
    using Linn.Common.Configuration;

    public static class LoggingConfiguration
    {
        public static string Environment => ConfigurationManager.Configuration["LOG_ENVIRONMENT"];

        public static int MaxInnerExceptionDepth => int.Parse(ConfigurationManager.Configuration["LOG_MAX_INNER_EXCEPTION_DEPTH"]);

        public static string AmazonSqsQueueUri => ConfigurationManager.Configuration["LOG_AMAZON_SQSQUEUEURI"];
    }
}
