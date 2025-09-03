namespace Linn.Portal.Authorization.Domain
{
    public class Privilege
    {
        public Privilege(string action)
        {
            this.IsActive = true;
            this.Action = action;
        }

        public Privilege()
        {
        }

        public int Id { get; set; }

        public string Action { get; set; }

        public bool IsActive { get; set; }
    }
}
