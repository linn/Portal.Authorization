namespace Linn.Portal.Authorization.Domain.Tests.SubjectTests
{
    using System;

    using FluentAssertions;

    using Linn.Portal.Authorization.Domain.Exceptions;

    using NUnit.Framework;

    using TestData;

    public class WhenCreatingPermissionAndUnauthorized
    {
        private Action act;

        private string grantedByGuid;

        private Association association;

        [SetUp]
        public void SetUp()
        {
            this.grantedByGuid = Guid.NewGuid().ToString();
            var privilege = new Privilege("view:back-orders", AssociationType.Retailer);
            var sub = new Subject(new Guid().ToString());
            this.association = new Association(sub, new Uri("/accounts/123", UriKind.RelativeOrAbsolute), AssociationType.Retailer);
            var grantedBy = new Subject(this.grantedByGuid);

            this.act = () =>
                {
                    sub.AddPermission(privilege, this.association, grantedBy);
                };
        }

        [Test]
        public void ShouldThrow()
        {
            this.act.Should().Throw<UnauthorisedActionException>().WithMessage(
                $"Subject {grantedByGuid} does not have permission to create permissions associated to {association.AssociatedResource}");
        }
    }
}
