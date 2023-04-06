using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Auth
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<ResponseUserAdminDetails>> AdminRegister(Admin newAdmin, string password);
        Task<ServiceResponse<ResponseUserStudentDetails>> StudentRegister(RequestAddUserStudent newStudent, string password);
        Task<ServiceResponse<ResponseAuth>> Login(RequestUserLogin user);
        Task<bool> UserExists(UserTypes userType, string userName);
    }
}