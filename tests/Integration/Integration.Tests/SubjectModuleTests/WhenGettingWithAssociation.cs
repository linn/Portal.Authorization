namespace Linn.Portal.Authorization.Integration.Tests.SubjectModuleTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;

    using FluentAssertions;

    using Linn.Portal.Authorization.Domain;
    using Linn.Portal.Authorization.Integration.Tests.Extensions;
    using Linn.Portal.Authorization.Resources;

    using NSubstitute;

    using NUnit.Framework;

    using TestData;

    public class WhenGettingWithAssociation : ContextBase
    {
        private Guid subjectId;

        private Subject subject;

        private Uri retailerUri;

        [SetUp]
        public void SetUp()
        {
            this.subjectId = Guid.NewGuid();
            this.retailerUri = new Uri("/retailers/123", UriKind.RelativeOrAbsolute);

            this.subject = new Subject(this.subjectId.ToString());
            var association = new Association(this.subject, this.retailerUri, AssociationType.Retailer);
            var grantor = new TestPermissionCreatorSubject(association);

            var privilege = new Privilege(AuthorisedActions.ViewInvoices, association.Type);
            this.subject.AddAssociation(association);
            this.subject.AddPermission(privilege, association, grantor);

            this.Repository.FilterBy(Arg.Any<Expression<Func<Subject, bool>>>()).Returns(
                new List<Subject>
                    {
                        this.subject
                    });

            this.Response = this.Client.Get(
                $"/portal-authorization/subjects?associationUri={this.retailerUri}",
                with =>
                {
                    with.Accept("application/json");
                }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnResults()
        {
            var res = this.Response.DeserializeBody<IEnumerable<SubjectResource>>().ToList();
            res.Count().Should().Be(1);
            res.All(x => x.Links.Any(l => l.Rel == "associated-retailer" && l.Href == this.retailerUri.ToString()))
                .Should().BeTrue();
        }
    }
}
