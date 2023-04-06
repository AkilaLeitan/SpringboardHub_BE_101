using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.SyllabusService
{
    public interface ISyllabusService
    {
        Task<ServiceResponse<ICollection<ResponseSyllabusDetails>>> GetAllSyllabus();
        Task<ServiceResponse<ResponseSyllabusDetails>> GetSyllabusByID(string uid);
        Task<ServiceResponse<ResponseSyllabusDetails>> AddSyllabus(RequestAddSyllabus newSyllabus);
        Task<ServiceResponse<ResponseSyllabusDetails>> UpdateSyllabus(RequestUpdateSyllabus updateSyllabus);
    }
}