using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpringboardHub_BE_101.Service.StudentService;

namespace SpringboardHub_BE_101.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        [HttpGet("GetAllStudents")]
        public async Task<ActionResult<ServiceResponse<List<List<ResponseUserStudentDetails>>>>> GetAllCourses()
        {
            return Ok(await _studentService.GetAllStudents());
        }

        [HttpGet("GetStudentByID")]
        public async Task<ActionResult<ServiceResponse<ResponseUserStudentDetails>>> GetStudentByID(string uid)
        {
            return Ok(await _studentService.GetStudentByID(uid));
        }

        [HttpGet("GetStudentsByBatch")]
        public async Task<ActionResult<ServiceResponse<ICollection<ResponseUserStudentDetails>>>> GetStudentsByBatch(string batchUID)
        {
            return Ok(await _studentService.GetStudentsByBatch(batchUID));
        }

        [HttpPut("UpdateStudent")]
        public async Task<ActionResult<ServiceResponse<ResponseUserStudentDetails>>> UpdateStudent(RequestUpdateUserStudent updateStudent)
        {
            return Ok(await _studentService.UpdateStudent(updateStudent));
        }

        [HttpDelete("DeleteStudent")]
        public async Task<ActionResult<ServiceResponse<NoParam>>> DeleteStudent(string uid)
        {
            return Ok(await _studentService.DeleteStudent(uid));
        }
    }
}