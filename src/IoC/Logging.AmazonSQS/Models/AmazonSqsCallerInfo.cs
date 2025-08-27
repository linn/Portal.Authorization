namespace Linn.Portal.Authorization.IoC.Logging.AmazonSQS.Models
{
    public class AmazonSqsCallerInfo
    {
        public string Class { get; set; }

        public string Method { get; set; }

        public string File { get; set; }

        public int Line { get; set; }
    }
}