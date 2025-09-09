namespace Linn.Portal.Authorization.Domain.Tests.SubjectTests.PermissionTests
{
    using System;

    using FluentAssertions;

    using Linn.Portal.Authorization.Domain.Exceptions;

    using NUnit.Framework;

    public class WhenCreatingAndPrivilegeScopeTypeDoesNotMathAssociationType
    {
        private Action act;

        [SetUp]
        public void SetUp()
        {
            var privilege = new Privilege("view:back-orders", "retailer");
            var sub = new Subject(new Guid().ToString());
            var association = new Association(sub, new Uri("/accounts/123", UriKind.RelativeOrAbsolute), "account");

            this.act = () =>
                {
                    new Permission(privilege, sub, association);
                };
        }

        [Test]
        public void ShouldThrow()
        {
            this.act.Should().Throw<CreatePermissionException>().WithMessage("view:back-orders is only applicable to associations of type retailer");
        }
    }
}
