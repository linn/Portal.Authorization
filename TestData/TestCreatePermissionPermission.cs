namespace TestData
{
    using System;

    using Linn.Portal.Authorization.Domain;

    public class TestCreatePermissionPermission : Permission
    {
        public TestCreatePermissionPermission(Association association)
        {
            this.Subject = new Subject(new Guid().ToString());
            this.GrantedBy = new TestAuthAdminSubject();
            this.Association = association;
            this.IsActive = true;
            this.Privilege = new Privilege(AuthorisedActions.CreatePermission, AssociationType.System);
        }
    }
}
