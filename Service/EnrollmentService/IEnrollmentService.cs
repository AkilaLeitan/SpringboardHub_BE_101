using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.EnrollmentService
{
    public interface IEnrollmentService
    {
        Task<ServiceResponse<ICollection<ResponseEnrollementDetails>>> GetAllEnrollemnts();
        Task<ServiceResponse<ResponseEnrollementDetails>> GetEnrollementByID(int id);
        Task<ServiceResponse<ResponseEnrollementDetails>> AddEnrollment(RequestAddEnrollemnt newEnrollement);
        Task<ServiceResponse<ResponseEnrollementDetails>> UpdateEnrollement(RequestUpdateEnrollement updateEnrollement);
    }
}