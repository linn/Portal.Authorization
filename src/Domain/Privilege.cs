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

        protected Privilege()
        {
        }

        public int Id { get; protected set; }

        public string Action { get; protected set; }

        public bool IsActive { get; protected set; }

        public AssociationType ScopeType { get; protected set; }

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
