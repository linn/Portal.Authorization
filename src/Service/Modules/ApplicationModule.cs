namespace Linn.Portal.Authorization.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Common.Service;
    using Linn.Common.Service.Extensions;
    using Linn.Portal.Authorization.Service.Models;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class ApplicationModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/", this.Redirect);
            app.MapGet("/portal-authorization", this.GetApp);
        }

        private Task Redirect(HttpRequest req, HttpResponse res)
        {
            res.Redirect("/portal-authorization");
            return Task.CompletedTask;
        }

        private async Task GetApp(HttpRequest req, HttpResponse res)
        {
            await res.Negotiate(new ViewResponse { ViewName = "Index.cshtml" });
        }
    }
}
