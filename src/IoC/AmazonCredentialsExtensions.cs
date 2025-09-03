namespace Linn.Portal.Authorization.IoC
{
    using System;

    using Amazon;
    using Amazon.Runtime;
    using Amazon.Runtime.CredentialManagement;
    using Amazon.SQS;

    using Linn.Common.Configuration;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class AmazonCredentialsExtensions
    {
        public static IServiceCollection AddCredentialsExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        }
    }
}
