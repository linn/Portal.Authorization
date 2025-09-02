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
    }
}
