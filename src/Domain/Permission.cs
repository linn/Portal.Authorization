namespace Linn.Portal.Authorization.Domain
{
    public class Permission
    {
        public int Id { get; set; }

        public Privilege Privilege { get; set; }

        public Subject Subject { get; set; }

        public bool IsActive { get; set; }
    }
}
