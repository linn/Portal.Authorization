namespace Linn.Portal.Authorization.Domain
{
    public class AuthorisedActions
    {
        public const string ViewInvoices = "invoices:view";

        public const string CreatePermission = "permissions:create";

        // the privilege that allows assignment of CreatePermission
        // i.e. lets a user set other users up with the power to assign other permissions
        public const string AuthAdmin = "authorization:admin"; 
    }
}
