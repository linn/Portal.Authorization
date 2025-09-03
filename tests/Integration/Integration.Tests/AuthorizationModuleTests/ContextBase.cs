namespace Linn.Portal.Authorization.Integration.Tests.AuthorizationModuleTests
{
    using System.Net.Http;

    using Linn.Common.Persistence;
    using Linn.Portal.Authorization.Facade.Services;
    using Linn.Portal.Authorization.Integration.Tests;
    using Linn.Portal.Authorization.IoC;
    using Linn.Portal.Authorization.Persistence.Repositories;
    using Linn.Portal.Authorization.Service.Modules;

    using Microsoft.Extensions.DependencyInjection;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected HttpClient Client { get; set; }

        protected HttpResponseMessage Response { get; set; }

        protected ISubjectRepository Repository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Repository = Substitute.For<ISubjectRepository>();
            IAuthorizationService facadeService = new AuthorizationService(this.Repository);

            this.Client = TestClient.With<AuthorizationModule>(
                services =>
                    {
                        services.AddSingleton(facadeService);
                        services.AddHandlers();
                        services.AddRouting();
                    });
        }
    }
}
