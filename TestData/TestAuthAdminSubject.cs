namespace TestData
{
    using System;
    using System.Collections.Generic;

    using Linn.Portal.Authorization.Domain;

    public class TestAuthAdminSubject : Subject
    {
        public TestAuthAdminSubject()
            : base(Guid.NewGuid(), new[] { new TestAuthAdminPermission() })
        {
        }
    }
}
