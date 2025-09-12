namespace Linn.Portal.Authorization.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Portal.Authorization.Domain.Exceptions;

    public class Subject
    {
        public Subject(string sub)
        {
            this.Sub = Guid.Parse(sub);
            this.Associations = new List<Association>();
            this.Permissions = new List<Permission>();
        }

        protected Subject()
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

            return this.Permissions.Any(p =>
                p.IsActive &&
                p.Privilege.IsActive &&
                p.Privilege.Action == attemptedAction &&
                p.Privilege.AppliesToAssociation(p.Association) &&
                (p.Association == null || p.Association.AssociatedResource == associationUri));
        }

        public void AddAssociation(Association toAdd)
        {
            this.Associations.Add(toAdd);
        }

        public void AddPermission(Privilege privilege, Association association, Subject grantedBy)
        {
            if (association.Type != privilege.ScopeType)
            {
                throw new CreatePermissionException(
                    $"{privilege.Action} is only applicable to associations of type {privilege.ScopeType}");
            }

            if (privilege.Action == AuthorisedActions.CreatePermission)
            {
                if (!grantedBy.HasPermissionFor(AuthorisedActions.AuthAdmin, null))
                {
                    throw new UnauthorisedActionException(
                        $"Subject {grantedBy.Sub} is not authorised to assign {AuthorisedActions.CreatePermission}");
                }
            }
            else if (!grantedBy.HasPermissionFor(AuthorisedActions.CreatePermission, association.AssociatedResource))
            {
                throw new UnauthorisedActionException(
                    $"Subject {grantedBy.Sub} does not have permission to create permissions associated to {association.AssociatedResource}");
            }

            // todo - check if they already have the permission?

            this.Permissions.Add(new Permission(
                privilege, this, association, grantedBy));
        }
    }
}
