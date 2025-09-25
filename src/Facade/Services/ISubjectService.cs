namespace Linn.Portal.Authorization.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Linn.Common.Facade;
    using Linn.Portal.Authorization.Resources;

    public interface ISubjectService
    {
        Task<IResult<SubjectResource>> AddSubject(string sub);
        
        Task<IResult<SubjectResource>> GetSubject(string sub);

        Task<IResult<IEnumerable<SubjectResource>>> GetSubjectsWithAssociation(Uri associationUri);
    }
}
