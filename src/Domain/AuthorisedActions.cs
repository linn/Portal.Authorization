namespace Linn.Portal.Authorization.Domain
{
    public class AuthorisedActions
    {
        public const string ViewInvoices = "invoices:view";

        public const string ManagePermissions = "permissions:manage";

        // the privilege that allows assignment of ManagePermissions
        // i.e. lets a user set other users up with the power to assign other association-scoped permissions 
        public const string AuthAdmin = "authorization:admin"; 
    }
}
