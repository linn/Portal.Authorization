namespace Linn.Portal.Authorization.Facade.Services
{
    using System;
    using System.Threading.Tasks;

    using Linn.Common.Facade;
    using Linn.Portal.Authorization.Persistence.Repositories;

    public class AuthorizationService : IAuthorizationService
    {
        private readonly ISubjectRepository subjectRepository;

        public AuthorizationService(ISubjectRepository subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        public async Task<IResult<bool>> HasPermissionFor(string sub, string privilege, Uri resource)
        {
            var subject = await this.subjectRepository.GetById(sub);

            if (subject.HasPermissionFor(privilege, resource))
            {
                return new SuccessResult<bool>(true);
            }
            else
            {
                return new UnauthorisedResult<bool>(
                    $"Subject {sub} does not have permission to perform {privilege} for {resource}");
            }
        }
    }
}
