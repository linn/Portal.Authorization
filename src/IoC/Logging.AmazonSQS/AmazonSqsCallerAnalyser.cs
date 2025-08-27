namespace Linn.Portal.Authorization.IoC.Logging.AmazonSQS
{
    using Models;

    public class AmazonSqsCallerAnalyser
    {
        private readonly int skipFrames;

        public AmazonSqsCallerAnalyser(int skipFrames = 0)
        {
            this.skipFrames = skipFrames;
        }

        public AmazonSqsCallerInfo GetCallerInfo()
        {
            return new AmazonSqsCallerInfo { Class = "unknown", Method = "unknown", File = "unknown", Line = 0 };
        }

        /*
            Awaiting reintroduction of StackTrace functionality into dotNetCore

            public AmazonSqsCallerInfo GetCallerInfo()
            {
                var stackTrace = new StackTrace(true);
                var stackFrame = stackTrace.GetFrames()?.Skip(this.skipFrames + 1).First();
                var methodBase = stackFrame?.GetMethod();
                var classValue = methodBase?.DeclaringType?.FullName;
                var methodValue = methodBase?.Name;
                var filePath = stackFrame?.GetFileName();
                var fileValue = Path.GetFileName(filePath);
                var lineValue = stackFrame?.GetFileLineNumber() ?? 0;

                return new AmazonSqsCallerInfo { Class = classValue, Method = methodValue, File = fileValue, Line = lineValue };
            }
        */
    }
}