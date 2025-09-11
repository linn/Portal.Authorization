namespace TestData
{
    using System;
    using System.Collections.Generic;

    using Linn.Portal.Authorization.Domain;

    public class TestAuthAdminSubject : Subject
    {
        public TestAuthAdminSubject()
        {
            var permission = new TestAuthAdminPermission();
            this.Sub = permission.Subject.Sub;
            this.Permissions = new List<Permission>
                                   {
                                       permission
                                   };
        }
    }
}