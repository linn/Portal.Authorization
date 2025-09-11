namespace TestData
{
    using System;
    using System.Collections.Generic;

    using Linn.Portal.Authorization.Domain;

    public class TestPermissionCreatorSubject : Subject
    {
        public TestPermissionCreatorSubject(Association association, Guid? guid = null)
        {
            this.Sub = guid ?? Guid.NewGuid();
            var permission = new TestCreatePermissionPermission(association);
            this.Sub = permission.Subject.Sub;
            this.Permissions = new List<Permission>
                                   {
                                       permission
                                   };
        }
    }
}
