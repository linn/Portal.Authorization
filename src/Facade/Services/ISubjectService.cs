namespace Linn.Portal.Authorization.Facade.Services
{
    using System.Threading.Tasks;

    using Linn.Common.Facade;
    using Linn.Portal.Authorization.Resources;

    public interface ISubjectService
    {
        Task<IResult<SubjectResource>> AddSubject(string sub);
    }
}
