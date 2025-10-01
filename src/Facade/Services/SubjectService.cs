namespace Linn.Portal.Authorization.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Resources;
    using Linn.Portal.Authorization.Domain;
    using Linn.Portal.Authorization.Persistence.Repositories;
    using Linn.Portal.Authorization.Resources;

    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository subjectRepository;

        private readonly ITransactionManager transactionManager;

        public SubjectService(ISubjectRepository subjectRepository, ITransactionManager transactionManager)
        {
            this.subjectRepository = subjectRepository;
            this.transactionManager = transactionManager;
        }

        public async Task<IResult<SubjectResource>> AddSubject(string sub)
        {
            try
            {
                var subject = new Subject(sub);

                await this.subjectRepository.AddSubject(subject);
                await this.transactionManager.CommitAsync();

                return new CreatedResult<SubjectResource>(new SubjectResource { Sub = sub });
            }
            catch (Exception ex)
            {
                return new BadRequestResult<SubjectResource>(ex.Message);
            }
        }
        
        public async Task<IResult<SubjectResource>> GetSubject(string sub)
        {
            var subject = await this.subjectRepository.GetById(sub);
            return new SuccessResult<SubjectResource>(BuildResource(subject));
        }

        public async Task<IResult<IEnumerable<SubjectResource>>> GetSubjectsWithAssociation(Uri associationUri)
        {
            var results = await this.subjectRepository.FilterBy(
                x => x.Associations.Any(a => a.IsActive && a.AssociatedResource == associationUri));
            return new SuccessResult<IEnumerable<SubjectResource>>(results.Select(BuildResource));
        }

        private static SubjectResource BuildResource(Subject subject)
        {
            return new SubjectResource
                       {
                           Sub = subject.Sub.ToString(),
                           Name = subject.Name,
                           Email = subject.Email,
                           Permissions = subject.Permissions.Select(
                               p => new PermissionResource
                                        {
                                            Sub = p.Subject.Sub.ToString(),
                                            GrantedBySub = p.GrantedBy.Sub.ToString(),
                                            PrivilegeId = p.Privilege.Id,
                                            AssociationUri = p.Association?.AssociatedResource,
                                            PrivilegeAction = p.Privilege.Action,
                                            AssociationId = p.Association?.Id,
                                            IsActive = p.IsActive && p.Privilege.IsActive
                                        }),
                           Links = subject.Associations?.Where(x => x.IsActive).Select(
                               a => new LinkResource
                                        {
                                            Rel = $"associated-{a.Type.ToString().ToLower()}",
                                            Href = a.AssociatedResource.ToString()
                                        }).ToArray()
                       };
        }
    }
}
