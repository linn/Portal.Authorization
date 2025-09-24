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
