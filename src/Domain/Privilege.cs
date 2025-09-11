namespace Linn.Portal.Authorization.Domain
{
    public class Privilege
    {
        public Privilege(string action, AssociationType scopeType)
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

        public AssociationType ScopeType { get; set; }

        public bool AppliesToAssociation(Association association)
        {
            return this.ScopeType switch
                {
                    AssociationType.System => true,
                    AssociationType.Retailer => association != null && association.Type == AssociationType.Retailer,
                    AssociationType.Account => association != null && association.Type == AssociationType.Account,
                    _ => false
                };
        }

    }
}
