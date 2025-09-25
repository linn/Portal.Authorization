namespace Linn.Portal.Authorization.Resources
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class SubjectResource : HypermediaResource
    {
        public string Sub { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public IEnumerable<PermissionResource> Permissions { get; set; }
    }
}
