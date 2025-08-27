namespace Linn.Portal.Authorization.IoC.Logging.AmazonSQS
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public static class AmazonSqsLogExceptionTransformer
    {
        public static AmazonSqsLogExceptionModel Convert(int maxDepth, Exception ex)
        {
            if (ex == null)
            {
                return null;
            }

            return new AmazonSqsLogExceptionModel
            {
                Type = ex.GetType().FullName,
                Message = ex.Message,
                Source = ex.Source,
                Target = GenerateTarget(ex),
                Data = GenerateData(ex).ToArray(),
                Stack = GenerateStack(ex).ToArray(),
                InnerExceptions = GenerateExceptions(maxDepth, ex).ToArray()
            };
        }

        private static IEnumerable<string> GenerateData(Exception ex)
        {
            return from DictionaryEntry entry in ex.Data select $"{entry.Key}='{entry.Value}'";
        }

        private static string GenerateTarget(Exception ex)
        {
            return "unknown";
        }

        /*
            Awaiting reintroduction of TargetSite functionality into dotNetCore

            private static string GenerateTarget(Exception ex)
            {
                return "unknown";
                ex.TargetSite?.Name;
            }
        */

        private static IEnumerable<string> GenerateStack(Exception ex)
        {
            return ex.StackTrace.Split('\n').Select(line => line.Trim());
        }

        private static IEnumerable<AmazonSqsLogExceptionModel> GenerateExceptions(int maxDepth, Exception ex)
        {
            if (maxDepth == 0)
            {
                yield break;
            }

            var aggregateException = ex as AggregateException;

            if (aggregateException != null)
            {
                foreach (var innerException in aggregateException.InnerExceptions)
                {
                    yield return Convert(maxDepth - 1, innerException);
                }
            }
            else
            {
                if (ex.InnerException != null)
                {
                    yield return Convert(maxDepth - 1, ex.InnerException);
                }
            }
        }
    }
}