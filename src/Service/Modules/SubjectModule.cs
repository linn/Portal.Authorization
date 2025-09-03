namespace Linn.Portal.Authorization.Service.Modules
{
    using System.Linq;
    using System.Threading.Tasks;

    using Linn.Common.Configuration;
    using Linn.Common.Facade;
    using Linn.Common.Service;
    using Linn.Common.Service.Extensions;
    using Linn.Portal.Authorization.Domain.Exceptions;
    using Linn.Portal.Authorization.Facade.Services;
    using Linn.Portal.Authorization.Resources;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class SubjectModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/portal-authorization/subjects", this.PostSubject);
            endpoints.MapGet("/portal-authorization/subjects/{subjectId}", this.GetSubject);
        }

        private async Task PostSubject(
            HttpRequest req,
            HttpResponse res,
            SubjectResource resource,
            ISubjectService service)
        {
            var apiKey = ConfigurationManager.Configuration["AUTHORIZATION_API_KEY"];

            if (!req.Headers.ContainsKey("X-shared-secret") || req.Headers["X-shared-secret"] != apiKey)
            {
                await res.Negotiate(new UnauthorisedResult<SubjectResource>("API key is invalid or missing"));
            }
            else
            {
                var result = await service.AddSubject(resource.Sub);

                await res.Negotiate(result);
            }
        }
        
        private async Task GetSubject(
            HttpRequest req,
            HttpResponse res,
            string subjectId,
            ISubjectService service)
        {
            var user = req.HttpContext.User;

            // we only want to service the request if the subjectId for which information is requested
            // matches the value of the sub claim for the authenticated user
            if (user.Identity?.IsAuthenticated != true)
            {
                res.StatusCode = StatusCodes.Status401Unauthorized;
                await res.WriteAsync("Not authenticated");
                return;
            }

            var sub = user.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if (sub != subjectId)
            {
                res.StatusCode = StatusCodes.Status401Unauthorized;
                await res.WriteAsync("Not authenticated");
                return;
            }

            var result = await service.GetSubject(sub);
            await res.Negotiate(result);
        }
    }
}
