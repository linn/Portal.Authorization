namespace Linn.Portal.Authorization.IoC
{
    using System.Collections.Generic;

    using Linn.Common.Service.Handlers;
    using Linn.Portal.Authorization.Resources;

    using Microsoft.Extensions.DependencyInjection;

    public static class HandlerExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            return services
                .AddSingleton<IHandler, JsonResultHandler<SubjectResource>>()
                .AddSingleton<IHandler, JsonResultHandler<IEnumerable<SubjectResource>>>()
                .AddSingleton<IHandler, JsonResultHandler<AuthorizationQueryResultResource>>()
                .AddSingleton<IHandler, JsonResultHandler<PermissionResource>>()
                .AddSingleton<IHandler, JsonResultHandler<IEnumerable<PrivilegeResource>>>();
        }
    }
}
