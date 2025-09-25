using Linn.Portal.Authorization.Domain;

namespace Linn.Portal.Authorization.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Common.Service;
    using Linn.Common.Service.Extensions;
    using Linn.Portal.Authorization.Facade.Services;
    using Linn.Portal.Authorization.Resources;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class AuthorizationModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/portal-authorization/check-authorization", this.CheckAuth);
            endpoints.MapPost("/portal-authorization/permissions", this.CreatePermission);
            endpoints.MapGet("/portal-authorization/privileges", this.GetPrivileges);
        }

        private async Task CheckAuth(
            HttpRequest req,
            HttpResponse res,
            AuthorisationQueryResource resource,
            IAuthorizationService service)
        {
            await res.Negotiate(
                await service.HasPermissionFor(
                    resource.Sub, 
                    resource.AttemptedAction, 
                    resource.AssociationUri));
        }

        private async Task CreatePermission(
            HttpRequest req,
            HttpResponse res,
            PermissionResource resource,
            IAuthorizationService service)
        {
            await res.Negotiate(
                await service.CreatePermission(resource));
        }

        private async Task GetPrivileges(
            HttpRequest req,
            HttpResponse res,
            IAuthorizationService service,
            AssociationType scopeType)
        {
            await res.Negotiate(
                await service.GetPrivileges(scopeType));
        }
    }
}
