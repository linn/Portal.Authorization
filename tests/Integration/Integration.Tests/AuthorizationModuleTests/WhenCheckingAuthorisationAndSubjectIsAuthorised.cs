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

    public class WhenCheckingAuthorisationAndSubjectIsAuthorised : ContextBase
    {
        private AuthorisationQueryResource resource;

        private Subject subject;

        [SetUp]
        public void SetUp()
        {
            var guid = Guid.NewGuid();
            var retailerUri = new Uri("/retailers/123", UriKind.RelativeOrAbsolute);
            this.subject = new Subject(guid.ToString());
            var association = new Association(this.subject, retailerUri, AssociationType.Retailer);
            var privilege = new Privilege(AuthorisedActions.ViewInvoices, association.Type);
            var permission = new Permission(privilege, this.subject, association, new TestPermissionCreatorSubject(association));
            this.subject.AddAssociation(association);
            this.subject.AddPermission(permission);

            this.resource = new AuthorisationQueryResource
                                {
                                    AssociationUri = retailerUri,
                                    AttemptedAction = privilege.Action,
                                    Sub = guid.ToString()
                                };

            this.SubjectRepository.GetById(guid.ToString()).Returns(this.subject);

            this.Response = this.Client.PostAsJsonAsync(
                "/portal-authorization/check-authorization", 
                this.resource).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
