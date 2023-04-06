using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<List<ICollection<ResponseUserStudentDetails>>>> GetAllStudents();
        Task<ServiceResponse<ResponseUserStudentDetails>> GetStudentByID(string uid);
        Task<ServiceResponse<ICollection<ResponseUserStudentDetails>>> GetStudentsByBatch(string batchUID);
        Task<ServiceResponse<ResponseUserStudentDetails>> UpdateStudent(RequestUpdateUserStudent updateStudent);
        Task<ServiceResponse<NoParam>> DeleteStudent(string uid);
    }
}