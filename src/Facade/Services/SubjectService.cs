namespace Linn.Portal.Authorization.Facade.Services
{
    using System;
    using System.Threading.Tasks;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
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
    }
}