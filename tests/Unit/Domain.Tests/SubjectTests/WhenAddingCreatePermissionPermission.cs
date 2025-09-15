namespace Linn.Portal.Authorization.Domain.Tests.SubjectTests
{
    using System;

    using FluentAssertions;

    using NUnit.Framework;

    using TestData;

    public class WhenAddingCreatePermissionPermission
    {
        private Subject sut;

        private Association association;

        private Privilege privilege;

        [SetUp]
        public void SetUp()
        {
            this.privilege = new Privilege(AuthorisedActions.CreatePermission, AssociationType.Retailer);
            this.sut = new Subject(Guid.NewGuid().ToString());
            this.association = new Association(this.sut, new Uri("/accounts/123", UriKind.RelativeOrAbsolute), AssociationType.Retailer);

            var grantedBy = new TestAuthAdminSubject();

            this.sut.AddPermission(this.privilege, this.association, grantedBy);
        }

        [Test]
        public void ShouldAdd()
        {
            this.sut.Permissions.Count.Should().Be(1);
        }
    }
}
