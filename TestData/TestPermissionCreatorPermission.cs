namespace TestData
{
    using System;

    using Linn.Portal.Authorization.Domain;

    public class TestPermissionCreatorPermission : Permission
    {
        public TestPermissionCreatorPermission()
        {
            this.Subject = new Subject(new Guid().ToString());
            this.GrantedBy = null;
            this.Association = null;
            this.Privilege = new Privilege(AuthorisedActions.AuthAdmin, null);
        }
    }
}
