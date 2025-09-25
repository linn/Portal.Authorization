namespace Linn.Portal.Authorization.Integration.Tests.AuthorizationModuleTests
{
    using System.Net.Http;

    using Linn.Common.Persistence;
    using Linn.Portal.Authorization.Domain;
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

        protected ISubjectRepository SubjectRepository { get; private set; }

        protected IRepository<Permission, int> PermissionRepository { get; private set; }

        protected IRepository<Privilege, int> PrivilegeRepository { get; private set; }

        protected IRepository<Association, int> AssociationRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.SubjectRepository = Substitute.For<ISubjectRepository>();
            this.PermissionRepository = Substitute.For<IRepository<Permission, int>>();
            this.PrivilegeRepository = Substitute.For<IRepository<Privilege, int>>();
            this.AssociationRepository = Substitute.For<IRepository<Association, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();

            IAuthorizationService facadeService = new AuthorizationService(
                this.SubjectRepository,
                this.PrivilegeRepository,
                this.AssociationRepository,
                this.TransactionManager);

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
