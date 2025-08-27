namespace Linn.Portal.Authorization.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Subject
    {
        public Guid Sub { get; set; }

        public ICollection<Association> Associations { get; set; }
        
        public ICollection<Permission> Permissions { get; set; }

        public bool HasPermissionFor(string privilege, Uri resource)
        {
            var hasAssociation = this.Associations.Any(a => a.AssociatedResource == resource);
            var hasPrivilege = this.Permissions.Any(p =>
                p.IsActive &&
                p.Privilege.IsActive &&
                p.Privilege.Action == privilege);

            return hasAssociation && hasPrivilege;
        }
    }
}
