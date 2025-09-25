namespace Linn.Portal.Authorization.Resources
{
    using System;

    public class PermissionResource
    {
        public string Sub { get; set; }

        public string GrantedBySub { get; set; }

        public int PrivilegeId { get; set; }

        public string PrivilegeAction { get; set; }

        public int? AssociationId { get; set; }

        public Uri AssociationUri { get; set; }
        
        public bool IsActive { get; set; }
    }
}
