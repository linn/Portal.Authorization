namespace Linn.Portal.Authorization.Domain.Tests.SubjectTests
{
    using System;

    using FluentAssertions;

    using Linn.Portal.Authorization.Domain.Exceptions;

    using NUnit.Framework;

    using TestData;

    public class WhenAddingCreatePermissionPermissionAndUnauthorized
    {
        private Action act;

        private Association association;

        private Privilege privilege;

        private Subject grantedBy;

        [SetUp]
        public void SetUp()
        {
            this.privilege = new Privilege(AuthorisedActions.ManagePermissions, AssociationType.Retailer);
            var sub = new Subject(Guid.NewGuid().ToString());
            this.association = new Association(sub, new Uri("/accounts/123", UriKind.RelativeOrAbsolute), AssociationType.Retailer);

            // a subject with the permission to create normal association scoped permissions,
            // but NOT the AuthAdmin permission that allows them to set up other permission creators
            this.grantedBy = new TestPermissionCreatorSubject(this.association);

            this.act = () => sub.AddPermission(this.privilege, this.association, this.grantedBy);
        }

        [Test]
        public void ShouldThrow()
        {
            this.act.Should().Throw<UnauthorisedActionException>().WithMessage(
                $"Subject {grantedBy.Sub} is not authorised to assign {AuthorisedActions.ManagePermissions}");
        }
    }
}
