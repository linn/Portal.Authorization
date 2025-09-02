namespace Linn.Portal.Authorization.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Common.Service;
    using Linn.Portal.Authorization.Facade.Services;
    using Linn.Portal.Authorization.Resources;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class AuthorizationModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/portal/authorization/subjects", this.CheckAuth);
        }

        private async Task CheckAuth(
            HttpRequest req,
            HttpResponse res,
            SubjectResource resource,
            ISubjectService service)
        {

        }
    }
}
