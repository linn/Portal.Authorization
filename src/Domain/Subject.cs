namespace Linn.Portal.Authorization.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Subject
    {
        public Subject(string sub)
        {
            this.Sub = Guid.Parse(sub);
            this.Associations = new List<Association>();
            this.Permissions = new List<Permission>();
        }

        public Subject()
        {
        }

        public Guid Sub { get; protected set; }

        public ICollection<Association> Associations { get; protected set; }
        
        public ICollection<Permission> Permissions { get; protected set; }

        public bool HasPermissionFor(string privilege, Uri resource)
        {
            var hasAssociation = this.Associations.Any(a => a.AssociatedResource == resource);
            var hasPrivilege = this.Permissions.Any(p =>
                p.IsActive &&
                p.Privilege.IsActive &&
                p.Privilege.Action == privilege);

            return hasAssociation && hasPrivilege;
        }

        public void AddAssociation(Association toAdd)
        {
            this.Associations.Add(toAdd);
        }

        public void AddPermission(Permission toAdd)
        {
            this.Permissions.Add(toAdd);
        }
    }
}
