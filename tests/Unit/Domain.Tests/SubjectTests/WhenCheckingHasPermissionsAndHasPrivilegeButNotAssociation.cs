namespace Linn.Portal.Authorization.Domain.Tests.SubjectTests
{
    using System;

    using FluentAssertions;

    using NUnit.Framework;

    public class WhenCheckingHasPermissionsAndHasPrivilegeButNotAssociation
    {
        private Subject sut;

        private Uri retailerUri;

        [SetUp]
        public void SetUp()
        {
            this.retailerUri = new Uri("/retailers/123", UriKind.RelativeOrAbsolute);
            this.sut = new Subject(Guid.NewGuid().ToString());

            var privilege = new Privilege(AuthorisedActions.ViewInvoices);

            var permission = new Permission(privilege, this.sut);

            this.sut.AddPermission(permission);
        }

        [Test]
        public void ShouldReturnFalse()
        {
            this.sut.HasPermissionFor(AuthorisedActions.ViewInvoices, this.retailerUri).Should().BeFalse();
        }
    }
}
