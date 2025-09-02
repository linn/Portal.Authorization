namespace Linn.Portal.Authorization.Persistence.Repositories
{
    using System.Threading.Tasks;

    using Linn.Portal.Authorization.Domain;

    public interface ISubjectRepository
    {
        Task<Subject> GetById(string sub);

        Task AddSubject(Subject toAdd);
    }
}
