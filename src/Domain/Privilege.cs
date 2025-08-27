namespace Linn.Portal.Authorization.Domain
{
    public class Privilege
    {
        public int Id { get; set; }

        public string Action { get; set; }

        public bool IsActive { get; set; }
    }
}
