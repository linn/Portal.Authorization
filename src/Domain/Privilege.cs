namespace Linn.Portal.Authorization.Domain
{
    public class Privilege
    {
        public Privilege(string action, string scopeType)
        {
            this.IsActive = true;
            this.Action = action;
            this.ScopeType = scopeType;
        }

        public Privilege()
        {
        }

        public int Id { get; set; }

        public string Action { get; set; }

        public bool IsActive { get; set; }

        public string ScopeType { get; set; }
    }
}
