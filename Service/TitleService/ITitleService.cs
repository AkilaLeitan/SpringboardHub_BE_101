using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.TitleService
{
    public interface ITitleService
    {
        Task<ServiceResponse<ICollection<ResponseTitleDetails>>> GetAllTitle();
        Task<ServiceResponse<ICollection<ResponseTitleDetails>>> GetTitleBySubject(string subjectUID);
        Task<ServiceResponse<ResponseTitleDetails>> GetTitleByID(string uid);
        Task<ServiceResponse<ResponseTitleDetails>> AddTitle(RequestAddTitle newTitle);
        Task<ServiceResponse<ResponseTitleDetails>> updateTitle(RequestUpdateTitle updateTitle);
    }
}