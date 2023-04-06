using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.SubjectService
{
    public interface ISubjectService
    {
        Task<ServiceResponse<ICollection<ResponseSubjectDetails>>> GetAllSubjects();
        Task<ServiceResponse<ResponseSubjectDetails>> GetSubjectByID(string uid);
        Task<ServiceResponse<ResponseSubjectDetails>> AddSubject(RequestAddSubject newSubject);
        Task<ServiceResponse<ResponseSubjectDetails>> UpdateSubject(RequestUpdateSubject updateSubject);
        Task<ServiceResponse<ResponseSubjectSyllabus>> SubjectAddToSyllabus(RequestSubjectToSyllabus subjectToSyllabus);
    }
}