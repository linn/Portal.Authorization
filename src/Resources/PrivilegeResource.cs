namespace Linn.Portal.Authorization.Resources
{
    public class PrivilegeResource
    {
        public int Id { get; set; }

        public string Action { get; set; }

        public bool IsActive { get; set; }
    }
}
