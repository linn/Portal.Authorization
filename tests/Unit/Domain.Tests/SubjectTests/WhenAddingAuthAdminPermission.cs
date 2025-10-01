namespace Linn.Portal.Authorization.Domain.Tests.SubjectTests
{
    using System;

    using FluentAssertions;

    using Linn.Portal.Authorization.Domain.Exceptions;

    using NUnit.Framework;

    using TestData;

    public class WhenAddingAuthAdminPermission
    {
        private Action act;

        private string grantedByGuid;
        
        [SetUp]
        public void SetUp()
        {
            var privilege = new Privilege(AuthorisedActions.AuthAdmin, AssociationType.System);
            var sub = new Subject(new Guid().ToString());
            var grantedBy = new TestAuthAdminSubject();

            this.act = () =>
            {
                sub.AddPermission(privilege, null, grantedBy);
            };
        }

        [Test]
        public void ShouldThrow()
        {
            this.act.Should().Throw<CreatePermissionException>().WithMessage(
                $"Cannot grant the {AuthorisedActions.AuthAdmin} privilege");
        }
    }
}