namespace Linn.Portal.Authorization.Persistence.Repositories
{
    using System;
    using System.Linq;

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

        public Subject GetById(string sub)
        {
            return this.serviceDbContext.Subjects
                .Include(x => x.Permissions).ThenInclude(p => p.Privilege)
                .Include(x => x.Associations)
                .FirstOrDefault(x => x.Sub == Guid.Parse(sub));
        }
    }
}
