namespace Linn.Portal.Authorization.Integration.Tests.AuthorizationModuleTests
{
    using System;
    using System.Net;
    using System.Net.Http.Json;

    using FluentAssertions;

    using Linn.Portal.Authorization.Domain;
    using Linn.Portal.Authorization.Resources;

    using NSubstitute;

    using NUnit.Framework;

    using TestData;

    public class WhenCreatingPermission : ContextBase
    {
        private PermissionResource resource;

        private Subject subject;

        private Subject grantedBy;

        private Association association;

        private Privilege privilege;

        [SetUp]
        public void SetUp()
        {
            var granteeGuid = Guid.NewGuid();
            var grantedByGuid = Guid.NewGuid();
            var retailerUri = new Uri("/retailers/123", UriKind.RelativeOrAbsolute);
            this.subject = new Subject(granteeGuid.ToString());
            this.association = new Association(this.subject, retailerUri, AssociationType.Retailer);

            this.grantedBy = new TestPermissionCreatorSubject(this.association, grantedByGuid);
            this.privilege = new Privilege(AuthorisedActions.ViewInvoices, this.association.Type);
            this.subject.AddAssociation(this.association);

            this.resource = new PermissionResource
                                {
                                   AssociationId = this.association.Id,
                                   GrantedBySub = grantedByGuid.ToString(),
                                   Sub = granteeGuid.ToString()
                                };

            this.SubjectRepository.GetById(granteeGuid.ToString()).Returns(this.subject);
            this.SubjectRepository.GetById(grantedByGuid.ToString()).Returns(this.grantedBy);

            this.AssociationRepository.FindByIdAsync(Arg.Any<int>()).Returns(this.association);
            this.PrivilegeRepository.FindByIdAsync(Arg.Any<int>()).Returns(this.privilege);
            this.Response = this.Client.PostAsJsonAsync(
                "/portal-authorization/permissions",
                this.resource).Result;
        }

        [Test]
        public void ShouldAdd()
        {
            this.PermissionRepository.Received(1).AddAsync(Arg.Is<Permission>(
                p => p.Subject.Sub.ToString() == this.subject.Sub.ToString()
                     && p.Privilege.Action == this.privilege.Action
                     && p.GrantedBy.Sub.ToString() == this.grantedBy.Sub.ToString()
                     && p.Association.AssociatedResource == this.association.AssociatedResource));
            this.TransactionManager.Received(1).CommitAsync();
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
