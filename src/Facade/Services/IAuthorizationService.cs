using Linn.Portal.Authorization.Domain;

namespace Linn.Portal.Authorization.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Linn.Common.Facade;
    using Linn.Portal.Authorization.Resources;

    public interface IAuthorizationService
    {
        Task<IResult<AuthorizationQueryResultResource>> HasPermissionFor(string sub, string privilege, Uri associationUri);

        Task<IResult<PermissionResource>> CreatePermission(PermissionResource toCreate);

        Task<IResult<IEnumerable<PrivilegeResource>>> GetPrivileges(AssociationType scopeType);
    }
}
