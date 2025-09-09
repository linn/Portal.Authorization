namespace Linn.Portal.Authorization.Domain
{
    public class Permission
    {
        public Permission(Privilege privilege, Subject sub, Association association)
        {
            this.Subject = sub;
            this.Privilege = privilege;
            this.IsActive = true;
            this.Association = association;
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
