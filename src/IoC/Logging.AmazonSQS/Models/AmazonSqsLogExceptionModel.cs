namespace Linn.Portal.Authorization.IoC.Logging.AmazonSQS.Models
{
    public class AmazonSqsLogExceptionModel
    {
        public string Type { get; set; }

        public string Message { get; set; }

        public string Source { get; set; }

        public string Target { get; set; }

        public string[] Data { get; set; }

        public string[] Stack { get; set; }

        public AmazonSqsLogExceptionModel[] InnerExceptions { get; set; }
    }
}