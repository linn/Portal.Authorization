namespace TestData
{
    using System;
    using System.Collections.Generic;
    using Linn.Portal.Authorization.Domain;

    public class TestPermissionCreatorSubject : Subject
    {
        public TestPermissionCreatorSubject(Association association, Guid? guid = null)
            : base(
                guid ?? Guid.NewGuid(),
                new List<Permission>
                    {
                        new TestCreatePermissionPermission(association)
                    })
        {
        }
    }
}
