namespace Linn.Portal.Authorization.Domain
{
    public class AuthorisedActions
    {
        public const string ViewInvoices = "invoices:view";

        public const string CreatePermission = "permissions:create";

        // special admin privilege
        // grants the power to give other subjects the power to create permissions with arbitrary association
        // as such is not scope to any association, and so is a sort of system level privilege
        public const string AuthAdmin = "authorization:admin";
    }
}
