namespace Linn.Portal.Authorization.Integration.Tests.SubjectModuleTests
{
    using System.Net.Http;

    using Linn.Common.Persistence;
    using Linn.Portal.Authorization.Facade.Services;
    using Linn.Portal.Authorization.Integration.Tests;
    using Linn.Portal.Authorization.IoC;
    using Linn.Portal.Authorization.Persistence.Repositories;
    using Linn.Portal.Authorization.Service.Modules;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.DependencyInjection;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected HttpClient Client { get; set; }

        protected HttpResponseMessage Response { get; set; }

        protected ISubjectRepository Repository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Repository = Substitute.For<ISubjectRepository>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            ISubjectService facadeService = new SubjectService(this.Repository, this.TransactionManager);

            this.Client = TestClient.With<SubjectModule>(
                services =>
                    {
                        services.AddSingleton(facadeService);
                        services.AddHandlers();
                        services.AddRouting();
                    });
        }
    }
}
