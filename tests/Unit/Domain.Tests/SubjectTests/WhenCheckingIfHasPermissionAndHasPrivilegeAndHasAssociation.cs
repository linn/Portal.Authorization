namespace Linn.Portal.Authorization.Domain.Tests.SubjectTests
{
    using System;

    using FluentAssertions;

    using NUnit.Framework;

    using TestData;

    public class WhenCheckingIfHasPermissionAndHasPrivilegeAndHasAssociation
    {
        private Subject sut;

        private Uri retailerUri;

        [SetUp]
        public void SetUp()
        {
            this.retailerUri = new Uri("/retailers/123", UriKind.RelativeOrAbsolute);
            this.sut = new Subject(Guid.NewGuid().ToString());

            var association = new Association(this.sut, this.retailerUri, "retailer");

            var privilege = new Privilege(AuthorisedActions.ViewInvoices, association.Type);

            var viewInvoicesPermission = new Permission(privilege, this.sut, association, new TestPermissionCreatorSubject());

            this.sut.AddAssociation(association);
            this.sut.AddPermission(viewInvoicesPermission);
        }

        [Test]
        public void ShouldReturnTrue()
        {
            this.sut.HasPermissionFor(AuthorisedActions.ViewInvoices, this.retailerUri).Should().BeTrue();
        }
    }
}
