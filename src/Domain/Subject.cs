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

        public bool HasPermissionFor(string attemptedAction, Uri associationUri)
        {
            if (this.Permissions == null || !this.Permissions.Any())
            {
                return false;
            }

            // are we trying to assign a subject the power to create permissions?
            if (attemptedAction == AuthorisedActions.CreatePermission)
            {
                // if so need the special admin privilege
                if (this.Permissions.Any(
                        x => x.Privilege.Action == AuthorisedActions.AuthAdmin && x.Privilege.IsActive))
                {
                    return true;
                }

                return false;
            }

            return this.Permissions.Any(p =>
                p.IsActive &&
                p.Privilege.IsActive &&
                p.Privilege.Action == attemptedAction
                && p.Association?.AssociatedResource == associationUri);
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
