namespace TestData
{
    using System;
    using System.Collections.Generic;

    using Linn.Portal.Authorization.Domain;

    public class TestPermissionCreatorSubject : Subject
    {
        public TestPermissionCreatorSubject()
        {
            var permission = new TestPermissionCreatorPermission();
            this.Sub = permission.Subject.Sub;
            this.Permissions = new List<Permission>
                                   {
                                       permission
                                   };
        }
    }
}
