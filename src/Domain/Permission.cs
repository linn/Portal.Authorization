namespace Linn.Portal.Authorization.Domain
{
    using Linn.Portal.Authorization.Domain.Exceptions;

    public class Permission
    {
        public Permission(Privilege privilege, Subject sub, Association association)
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
        }

        public Permission()
        {
        }

        public int Id { get; set; }

        public Privilege Privilege { get; set; }

        public Subject Subject { get; set; }

        public Association Association { get; set; }

        public bool IsActive { get; set; }
    }
}
