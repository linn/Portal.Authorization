namespace Linn.Portal.Authorization.Resources
{
    using System;

    public class AuthorisationQueryResource
    {
        public string Sub { get; set; }

        public string AttemptedAction { get; set; }

        public Uri AssociationUri { get; set; }
    }
}
