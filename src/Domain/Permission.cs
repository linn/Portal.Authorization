namespace Linn.Portal.Authorization.Domain
{
    using Linn.Portal.Authorization.Domain.Exceptions;

    public class Permission
    {
        public Permission(Privilege privilege, Subject sub, Association association, Subject grantedBy)
        {
            if (privilege.ScopeType != AssociationType.System
            && association.Type != privilege.ScopeType)
            {
                throw new CreatePermissionException(
                    $"{privilege.Action} is only applicable to associations of type {privilege.ScopeType}");
            }
            
            // the AuthAdmin privilege is special and cannot be granted through the normal application flow.
            // it must be bootstrapped manually for the initial AuthAdmin users.
            // in future could consider allowing existing AuthAdmins to grant it to others.
            if (privilege.Action == AuthorisedActions.AuthAdmin)
            {
                throw new CreatePermissionException($"Cannot grant the {AuthorisedActions.AuthAdmin} privilege");
            }

            this.Subject = sub;
            this.Privilege = privilege;
            this.IsActive = true;
            this.Association = association;
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
    }
}
