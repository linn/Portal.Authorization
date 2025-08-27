namespace Linn.Portal.Authorization.Domain
{
    public class AuthorisedActions
    {
        // do we really need magic strings like this? would it make more sense to just pull from db?
        public const string ViewInvoices = "invoices:view";
    }
}
