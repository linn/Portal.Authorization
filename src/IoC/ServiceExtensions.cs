namespace Linn.Portal.Authorization.IoC
{
    using Linn.Common.Rendering;

    using Microsoft.Extensions.DependencyInjection;

    using RazorEngineCore;

    public static class ServiceExtensions
    {
        public static IServiceCollection AddFacadeServices(this IServiceCollection services)
        {
            return services;
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
