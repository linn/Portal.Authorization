namespace Linn.Portal.Authorization.Domain
{
    using System;

    using Linn.Portal.Authorization.Domain.Exceptions;

    public class Permission
    {
        public Permission(Privilege privilege, Subject sub, Association association, Subject grantedBy)
        {
            this.Subject = sub;
            this.Privilege = privilege;
            this.IsActive = true;
            this.Association = association;

            if (this.Association.Type != this.Privilege.ScopeType)
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

            this.GrantedBy = grantedBy;
        }

        protected Permission()
        {
        }

        public int Id { get; protected set; }

        public Privilege Privilege { get; protected set; }

        public Subject Subject { get; protected set; }

        public Subject GrantedBy { get; protected set; }

        public Association Association { get; protected set; }

        public bool IsActive { get; protected set; }

        // EF will use these for FK mapping, not visible in domain
        private Guid SubjectId { get; set; }

        private Guid GrantedById { get; set; }
    }
}
