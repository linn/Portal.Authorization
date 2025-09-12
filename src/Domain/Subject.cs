namespace Linn.Portal.Authorization.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Portal.Authorization.Domain.Exceptions;

    public class Subject
    {
        private readonly List<Association> associations;

        private readonly List<Permission> permissions;

        public Subject(string sub)
        {
            this.Sub = Guid.Parse(sub);
            this.associations = new List<Association>();
            this.permissions = new List<Permission>();
        }

        // for ef core
        protected Subject()
        {
        }

        // only ever called by test data subclasses
        // to support seeding subjects for testing
        protected Subject(Guid sub, IEnumerable<Permission> permissions)
        {
            this.Sub = sub;
            this.associations = new List<Association>();
            this.permissions = new List<Permission>(permissions ?? Enumerable.Empty<Permission>());
        }

        public Guid Sub { get; protected set; }

        public IReadOnlyCollection<Association> Associations => this.associations.AsReadOnly();

        public IReadOnlyCollection<Permission> Permissions => this.permissions.AsReadOnly();

        public bool HasPermissionFor(string attemptedAction, Uri associationUri)
        {
            if (!this.permissions.Any())
            {
                return false;
            }

            return this.permissions.Any(p =>
                p.IsActive &&
                p.Privilege.IsActive &&
                p.Privilege.Action == attemptedAction &&
                p.Privilege.AppliesToAssociation(p.Association) &&
                (p.Association == null || p.Association.AssociatedResource == associationUri));
        }

        public void AddAssociation(Association toAdd)
        {
            this.associations.Add(toAdd);
        }

        public void AddPermission(Privilege privilege, Association association, Subject grantedBy)
        {
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

            if (this.permissions.Any(
                    p => p.Privilege.Action == privilege.Action
                         && p.Association.AssociatedResource == association.AssociatedResource))
            {
                throw new CreatePermissionException(
                    $"{this.Sub} already has permission to {privilege.Action} on {association.AssociatedResource}");
            }

            this.permissions.Add(new Permission(
                privilege, this, association, grantedBy));
        }
    }
}
