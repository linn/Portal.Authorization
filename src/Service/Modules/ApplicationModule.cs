namespace Linn.Portal.Authorization.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Common.Logging;
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
            app.MapGet("/portal-authorization/test-log", this.LogSomeStuff);
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

        private async Task LogSomeStuff(HttpRequest req, HttpResponse res, ILog log)
        {
            log.Info("Some info");
            log.Warning("Some warning");
            log.Error("An error");
            await res.WriteAsync("Enjoy your logs");
        }
    }
}
