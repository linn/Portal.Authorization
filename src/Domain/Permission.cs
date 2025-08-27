namespace Linn.Portal.Authorization.Domain
{
    public class Permission
    {
        public Permission(Privilege privilege, Subject sub)
        {
            this.Subject = sub;
            this.Privilege = privilege;
            this.IsActive = true;
        }

        public Permission()
        {
        }

        public int Id { get; set; }

        public Privilege Privilege { get; set; }

        public Subject Subject { get; set; }

        public bool IsActive { get; set; }
    }
}
