namespace Linn.Portal.Authorization.Domain.Tests.SubjectTests
{
    using System;

    using FluentAssertions;

    using NUnit.Framework;

    using TestData;

    public class WhenCheckingHasPermissionsAndHasPrivilegeButNotAssociation
    {
        private Subject sut;

        private Uri retailerUri;

        [SetUp]
        public void SetUp()
        {
            this.retailerUri = new Uri("/retailers/123", UriKind.RelativeOrAbsolute);
            this.sut = new Subject(Guid.NewGuid().ToString());

            var privilege = new Privilege(AuthorisedActions.ViewInvoices, "retailer");

            var permission = new Permission(
                privilege,
                this.sut,
                new Association(this.sut, new Uri("/retailers/456", UriKind.RelativeOrAbsolute), "retailer"),
                new TestPermissionCreatorSubject());

            this.sut.AddPermission(permission);
        }

        [Test]
        public void ShouldReturnFalse()
        {
            this.sut.HasPermissionFor(AuthorisedActions.ViewInvoices, this.retailerUri).Should().BeFalse();
        }
    }
}
