namespace Linn.Portal.Authorization.Integration.Tests.AuthorizationModuleTests
{
    using System;
    using System.Net;
    using System.Net.Http.Json;

    using FluentAssertions;

    using Linn.Portal.Authorization.Domain;
    using Linn.Portal.Authorization.Integration.Tests.Extensions;
    using Linn.Portal.Authorization.Resources;
    using NSubstitute;

    using NUnit.Framework;

    public class WhenCheckingAuthorisationAndSubjectIsNotAuthorised : ContextBase
    {
        private AuthorisationQueryResource resource;

        private Subject subject;

        [SetUp]
        public void SetUp()
        {
            var guid = Guid.NewGuid();
            var retailerUri = new Uri("/retailers/123", UriKind.RelativeOrAbsolute);
            this.subject = new Subject(guid.ToString());
            var association = new Association(this.subject, retailerUri);
            var privilege = new Privilege(AuthorisedActions.ViewInvoices);
            var permission = new Permission(privilege, this.subject);
            this.subject.AddAssociation(association);

            this.resource = new AuthorisationQueryResource
                                {
                                    AssociationUri = retailerUri,
                                    AttemptedAction = privilege.Action,
                                    Sub = guid.ToString()
                                };

            this.Repository.GetById(guid.ToString()).Returns(this.subject);

            this.Response = this.Client.PostAsJsonAsync(
                "/portal-authorization/check-authorization",
                this.resource).Result;
        }

        [Test]
        public void ShouldReturnUnauthorized()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public void ShouldReturnBody()
        {
            var res = this.Response.DeserializeBody<AuthorizationQueryResultResource>();

            res.IsAuthorized.Should().BeFalse();
            res.Message.Should().Be(
                $"Subject {this.subject.Sub.ToString()} does not have permission to perform {AuthorisedActions.ViewInvoices} for /retailers/123");
        }
    }
}
