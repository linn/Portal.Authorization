namespace Linn.Portal.Authorization.Integration.Tests.SubjectModuleTests
{
    using System;
    using System.Net;

    using FluentAssertions;

    using Linn.Portal.Authorization.Domain;
    using Linn.Portal.Authorization.Integration.Tests.Extensions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingByIdAndUnauthorised : ContextBase
    {
        private Guid subjectId;
        
        private Subject subject;
        
        [SetUp]
        public void SetUp()
        {
            this.subjectId = Guid.NewGuid();
            var retailerUri = new Uri("/retailers/123", UriKind.RelativeOrAbsolute);

            this.subject = new Subject(this.subjectId.ToString());
            var association = new Association(this.subject, retailerUri, "retailer");
            var privilege = new Privilege(AuthorisedActions.ViewInvoices, association.Type);
            var permission = new Permission(privilege, this.subject, association);
            this.subject.AddAssociation(association);
            this.subject.AddPermission(permission);
            
            this.Repository.GetById(this.subjectId.ToString()).Returns(this.subject);
            
            TestAuthHandler.SubClaimValue = new Guid().ToString(); // a different sub than the one thats logged in!

            this.Response = this.Client.Get(
                $"/portal-authorization/subjects/{this.subjectId.ToString()}",
                with =>
                    {
                        with.Accept("application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnUnauthorized()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
