namespace Linn.Portal.Authorization.Domain.Exceptions
{
    using System;

    public class CreatePermissionException : Exception
    {
        public CreatePermissionException(string message)
            : base(message)
        {
        }

        public CreatePermissionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
