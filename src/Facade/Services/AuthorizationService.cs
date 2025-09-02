namespace Linn.Portal.Authorization.Facade.Services
{
    using System;
    using System.Threading.Tasks;

    using Linn.Common.Facade;
    using Linn.Portal.Authorization.Persistence.Repositories;
    using Linn.Portal.Authorization.Resources;

    public class AuthorizationService : IAuthorizationService
    {
        private readonly ISubjectRepository subjectRepository;

        public AuthorizationService(ISubjectRepository subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        public async Task<IResult<AuthorizationQueryResultResource>> HasPermissionFor(string sub, string privilege, Uri associationUri)
        {
            var subject = await this.subjectRepository.GetById(sub);

            if (subject.HasPermissionFor(privilege, associationUri))
            {
                return new SuccessResult<AuthorizationQueryResultResource>(new AuthorizationQueryResultResource { IsAuthorized = true });
            }
            else
            {
                return new UnauthorisedResult<AuthorizationQueryResultResource>(
                    $"Subject {sub} does not have permission to perform {privilege} for {associationUri}");
            }
        }
    }
}
