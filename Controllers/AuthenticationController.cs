using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthRepository _authRepo;

        public AuthenticationController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register/Admin")]
        public async Task<ActionResult<ServiceResponse<ResponseUserAdminDetails>>> AdminRegister(RequestAddUserAdmin newAdmin)
        {
            var admin = new Admin
            {
                FirstName = newAdmin.FirstName,
                LastName = newAdmin.LastName,
                Telephone = newAdmin.Telephone,
                Email = newAdmin.Email,
                NIC = newAdmin.NIC
            };

            return Ok(await _authRepo.AdminRegister(admin, newAdmin.Password));
        }

        [HttpPost("Register/Lecture")]
        public async Task<ActionResult<ServiceResponse<ResponseUserLectureDetails>>> LectureRegister(RequestAddUserLecture newLecture)
        {
            var lecture = new Lecture
            {
                FirstName = newLecture.FirstName,
                LastName = newLecture.LastName,
                Telephone = newLecture.Telephone,
                Email = newLecture.Email,
                NIC = newLecture.NIC
            };
            return Ok(await _authRepo.LectureRegister(lecture, newLecture.Password));
        }

        [HttpPost("Register/Student")]
        public async Task<ActionResult<ServiceResponse<ResponseUserStudentDetails>>> StudentRegister(RequestAddUserStudent newStudent)
        {
            return Ok(await _authRepo.StudentRegister(newStudent, newStudent.Password));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(RequestUserLogin loginUser)
        {
            return Ok(await _authRepo.Login(loginUser));
        }

    }
}