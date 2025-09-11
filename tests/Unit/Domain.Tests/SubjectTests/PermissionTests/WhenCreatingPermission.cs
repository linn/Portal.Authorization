namespace Linn.Portal.Authorization.Domain.Tests.SubjectTests.PermissionTests
{
    using System;

    using FluentAssertions;

    using NUnit.Framework;

    using TestData;

    public class WhenCreatingPermission
    {
        private Permission sut;

        private Association association;

        private Privilege privilege;

        private Subject sub;

        [SetUp]
        public void SetUp()
        {
            this.privilege = new Privilege(AuthorisedActions.ViewInvoices, AssociationType.Retailer);
            this.sub = new Subject(Guid.NewGuid().ToString());
            this.association = new Association(this.sub, new Uri("/accounts/123", UriKind.RelativeOrAbsolute), AssociationType.Retailer);

            var grantedBy = new TestPermissionCreatorSubject(this.association);

            this.sut = new Permission(this.privilege, this.sub, this.association, grantedBy);
        }

        [Test]
        public void ShouldCreate()
        {
            this.sut.GrantedBy.Should().NotBe(null);
            this.sut.IsActive.Should().Be(true);
            this.sut.Privilege.Action.Should().Be(this.privilege.Action);
            this.sut.Subject.Sub.ToString().Should().Be(this.sub.Sub.ToString());
            this.sut.Association.AssociatedResource.Should().Be(this.association.AssociatedResource);
            this.sut.GrantedBy.Should().NotBeNull();
        }
    }
}