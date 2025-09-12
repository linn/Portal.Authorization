namespace Linn.Portal.Authorization.Domain.Tests.SubjectTests
{
    using System;

    using FluentAssertions;

    using NUnit.Framework;

    public class WhenCheckingHasPermissionsAndHasAssociationButNotPrivilege
    {
        private Subject sut;

        private Uri retailerUri;

        [SetUp]
        public void SetUp()
        {
            this.retailerUri = new Uri("/retailers/123", UriKind.RelativeOrAbsolute);
            this.sut = new Subject(Guid.NewGuid().ToString());

            var association = new Association(this.sut, this.retailerUri, AssociationType.Retailer);

            this.sut.AddAssociation(association);
        }

        [Test]
        public void ShouldReturnFalse()
        {
            this.sut.HasPermissionFor(AuthorisedActions.ViewInvoices, this.retailerUri).Should().BeFalse();
        }
    }
}
