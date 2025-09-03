namespace Linn.Portal.Authorization.Domain.Exceptions
{
    using System;

    public class UnauthorisedActionException : Exception
    {
        public UnauthorisedActionException(string message)
            : base(message)
        {
        }

        public UnauthorisedActionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
