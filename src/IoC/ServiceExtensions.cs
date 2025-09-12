namespace Linn.Portal.Authorization.IoC
{
    using Linn.Common.Facade;
    using Linn.Common.Rendering;
    using Linn.Portal.Authorization.Domain;
    using Linn.Portal.Authorization.Facade.Services;
    using Linn.Portal.Authorization.Resources;

    using Microsoft.Extensions.DependencyInjection;

    using RazorEngineCore;

    public static class ServiceExtensions
    {
        public static IServiceCollection AddFacadeServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ISubjectService, SubjectService>()
                .AddScoped<IAuthorizationService, AuthorizationService>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<IRazorEngine, RazorEngine>()
                .AddSingleton<ITemplateEngine, RazorTemplateEngine>();
        }

        public static IServiceCollection AddBuilders(this IServiceCollection services)
        {
            return services;
        }
    }
}
