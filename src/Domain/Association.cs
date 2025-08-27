namespace Linn.Portal.Authorization.Domain
{
    using System;

    public class Association
    {
        public Subject Subject { get; set; }
        
        public Uri AssociatedResource { get; set; }
        
        public bool IsActive { get; set; }

        public int Id { get; set; }
    }
}
