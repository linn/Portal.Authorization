namespace Linn.Portal.Authorization.Persistence.Repositories
{
    using Linn.Portal.Authorization.Domain;

    public interface ISubjectRepository
    {
        Subject GetById(string sub);
    }
}
