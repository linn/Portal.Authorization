namespace Linn.Portal.Authorization.Domain.Tests.SubjectTests
{
    using System;

    using FluentAssertions;

    using Linn.Portal.Authorization.Domain.Exceptions;

    using NUnit.Framework;

    using TestData;

    public class WhenAddingPermissionAndSubjectAlreadyHasPermission
    {
        private Association association;

        private Privilege privilege;

        private Subject sub;

        private Action act;

        [SetUp]
        public void SetUp()
        {
            this.privilege = new Privilege(AuthorisedActions.ViewInvoices, AssociationType.Retailer);
            this.sub = new Subject(Guid.NewGuid().ToString());
            this.association = new Association(this.sub, new Uri("/accounts/123", UriKind.RelativeOrAbsolute), AssociationType.Retailer);

            var grantedBy = new TestPermissionCreatorSubject(this.association);

            this.sub.AddPermission(this.privilege, this.association, grantedBy);

            // try add the same permission again
            this.act = () =>
                {
                    this.sub.AddPermission(this.privilege, this.association, grantedBy);
                };
        }

        [Test]
        public void ShouldAdd()
        {
            this.act.Should().Throw<CreatePermissionException>().WithMessage(
                $"{this.sub.Sub} already has permission to {this.privilege.Action} on {this.association.AssociatedResource}");
        }
    }
}