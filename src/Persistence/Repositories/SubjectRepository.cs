namespace Linn.Portal.Authorization.Persistence.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Linn.Portal.Authorization.Domain;
    using Linn.Portal.Authorization.Persistence;

    using Microsoft.EntityFrameworkCore;

    public class SubjectRepository : ISubjectRepository
    {
        private readonly ServiceDbContext serviceDbContext;

        public SubjectRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public async Task<Subject> GetById(string sub)
        {
            return await this.serviceDbContext.Subjects
                .Include(x => x.Permissions).ThenInclude(p => p.Privilege)
                .Include(x => x.Associations)
                .FirstOrDefaultAsync(x => x.Sub == Guid.Parse(sub));
        }

        public async Task AddSubject(Subject toAdd)
        {
            await this.serviceDbContext.Subjects.AddAsync(toAdd);
        }
    }
}
