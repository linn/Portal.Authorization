namespace TestData
{
    using System;

    using Linn.Portal.Authorization.Domain;

    public class TestAuthAdminPermission : Permission
    {
        public TestAuthAdminPermission()
        {
            this.Subject = new Subject(new Guid().ToString());
            this.GrantedBy = null;
            this.Association = null;
            this.IsActive = true;
            this.Privilege = new Privilege(AuthorisedActions.AuthAdmin, AssociationType.System);
        }
    }
}