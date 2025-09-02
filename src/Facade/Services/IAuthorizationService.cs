namespace Linn.Portal.Authorization.Facade.Services
{
    using System;
    using System.Threading.Tasks;

    using Linn.Common.Facade;

    public interface IAuthorizationService
    {
        Task<IResult<bool>> HasPermissionFor(string sub, string privilege, Uri resource);
    }
}
