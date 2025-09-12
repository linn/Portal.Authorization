namespace Linn.Portal.Authorization.Domain.Tests.SubjectTests
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using NUnit.Framework;

    using TestData;

    public class WhenAddingPermission
    {
        private Association association;

        private Privilege privilege;

        private Subject sut;

        [SetUp]
        public void SetUp()
        {
            this.privilege = new Privilege(AuthorisedActions.ViewInvoices, AssociationType.Retailer);
            this.sut = new Subject(Guid.NewGuid().ToString());
            this.association = new Association(this.sut, new Uri("/accounts/123", UriKind.RelativeOrAbsolute), AssociationType.Retailer);

            var grantedBy = new TestPermissionCreatorSubject(association);

            this.sut.AddPermission(this.privilege, this.association, grantedBy);
        }

        [Test]
        public void ShouldAdd()
        {
           this.sut.Permissions.Count.Should().Be(1);
           var added = this.sut.Permissions.First();
           added.Privilege.Action.Should().Be(this.privilege.Action);
           added.Association.AssociatedResource.Should().Be(this.association.AssociatedResource);
        }
    }
}