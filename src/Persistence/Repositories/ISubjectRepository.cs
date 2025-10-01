namespace Linn.Portal.Authorization.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Linn.Portal.Authorization.Domain;

    public interface ISubjectRepository
    {
        Task<Subject> GetById(string sub);

        Task AddSubject(Subject toAdd);

        Task<IEnumerable<Subject>> FilterBy(Expression<Func<Subject, bool>> filterExpr);
    }
}
