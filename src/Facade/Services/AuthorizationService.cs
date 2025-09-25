namespace Linn.Portal.Authorization.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Portal.Authorization.Domain;
    using Linn.Portal.Authorization.Domain.Exceptions;
    using Linn.Portal.Authorization.Persistence.Repositories;
    using Linn.Portal.Authorization.Resources;

    public class AuthorizationService : IAuthorizationService
    {
        private readonly ISubjectRepository subjectRepository;
        
        private readonly IRepository<Privilege, int> privilegeRepository;

        private readonly IRepository<Association, int> associationRepository;

        private readonly ITransactionManager transactionManager;

        public AuthorizationService(
            ISubjectRepository subjectRepository,
            IRepository<Privilege, int> privilegeRepository,
            IRepository<Association, int> associationRepository,
            ITransactionManager transactionManager)
        {
            this.subjectRepository = subjectRepository;
            this.privilegeRepository = privilegeRepository;
            this.associationRepository = associationRepository;
            this.transactionManager = transactionManager;
        }

        public async Task<IResult<AuthorizationQueryResultResource>> HasPermissionFor(string sub, string privilege, Uri associationUri)
        {
            var subject = await this.subjectRepository.GetById(sub);

            if (subject.HasPermissionFor(privilege, associationUri))
            {
                return new SuccessResult<AuthorizationQueryResultResource>(new AuthorizationQueryResultResource { IsAuthorized = true });
            }

            return new UnauthorisedResult<AuthorizationQueryResultResource>(
                new AuthorizationQueryResultResource
                    {
                        IsAuthorized = false,
                        Message = $"Subject {sub} does not have permission to perform {privilege} for {associationUri}"
                    });
        }

        public async Task<IResult<PermissionResource>> CreatePermission(PermissionResource toCreate)
        {
            var subject = await this.subjectRepository.GetById(toCreate.Sub);
            var grantedBy = await this.subjectRepository.GetById(toCreate.GrantedBySub);
            var privilege = await this.privilegeRepository.FindByIdAsync(toCreate.PrivilegeId);
            var association = await this.associationRepository.FindByIdAsync(toCreate.AssociationId.GetValueOrDefault());

            try
            {
                subject.AddPermission(privilege, association, grantedBy);
                await this.transactionManager.CommitAsync();

                return new CreatedResult<PermissionResource>(
                    new PermissionResource
                        {
                            AssociationId = association.Id,
                            GrantedBySub = grantedBy.Sub.ToString(),
                            PrivilegeId = privilege.Id,
                            Sub = subject.Sub.ToString()
                        });
            }
            catch (UnauthorisedActionException ex)
            {
                return new UnauthorisedResult<PermissionResource>(ex.Message);
            }
            catch (CreatePermissionException ex)
            {
                return new BadRequestResult<PermissionResource>(ex.Message);
            }
        }

        public async Task<IResult<IEnumerable<PrivilegeResource>>> GetPrivileges(AssociationType scopeType)
        {
            var result = await this.privilegeRepository.FindAllAsync();
            return new SuccessResult<IEnumerable<PrivilegeResource>>(result.Where(x => x.ScopeType == scopeType).Select(
                p => new PrivilegeResource
                         {
                             Action = p.Action,
                             IsActive = p.IsActive,
                             Id = p.Id
                         }));
        }
    }   
}
