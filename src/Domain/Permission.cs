namespace Linn.Portal.Authorization.Domain
{
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

            if (!grantedBy.HasPermissionFor(AuthorisedActions.CreatePermission, association.AssociatedResource))
            {
                throw new UnauthorisedActionException(
                    $"Subject {grantedBy.Sub} does not have permission to create permissions associated to {association.AssociatedResource}");
            }

            this.GrantedBy = grantedBy;
        }

        public Permission()
        {
        }

        public int Id { get; set; }

        public Privilege Privilege { get; set; }

        public Subject Subject { get; set; }

        public Subject GrantedBy { get; set; }

        public Association Association { get; set; }

        public bool IsActive { get; set; }
    }
}
