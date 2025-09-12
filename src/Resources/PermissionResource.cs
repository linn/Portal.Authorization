namespace Linn.Portal.Authorization.Resources
{
    using System;

    public class PermissionResource
    {
        public string Sub { get; set; }

        public string GrantedBySub { get; set; }

        public int PrivilegeId { get; set; }

        public int AssociationId { get; set; }
    }
}
