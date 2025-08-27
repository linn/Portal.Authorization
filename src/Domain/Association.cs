namespace Linn.Portal.Authorization.Domain
{
    using System;

    public class Association
    {
        public Association(Subject sub, Uri associatedResource)
        {
            this.IsActive = true;
            this.Subject = sub;
            this.AssociatedResource = associatedResource;
        }

        public Association()
        {
        }

        public Subject Subject { get; protected set; }
        
        public Uri AssociatedResource { get; protected set; }
        
        public bool IsActive { get; protected set; }

        public int Id { get; protected set; }
    }
}
