namespace Linn.Portal.Authorization.IoC
{
    using Linn.Common.Rendering;
    using Linn.Portal.Authorization.Facade.Services;

    using Microsoft.Extensions.DependencyInjection;

    using RazorEngineCore;

    public static class ServiceExtensions
    {
        public static IServiceCollection AddFacadeServices(this IServiceCollection services)
        {
            return services.AddScoped<ISubjectService, SubjectService>();
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
