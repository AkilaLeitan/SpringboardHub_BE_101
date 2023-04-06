using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpringboardHub_BE_101.Service.CourseService;

namespace SpringboardHub_BE_101.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : BaseController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            this._courseService = courseService;
        }

        [HttpGet("GetAllCourses")]
        public async Task<ActionResult<ServiceResponse<ICollection<ResponseCourseDetails>>>> GetAllCourses()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value == UserTypes.Admin.ToString())
            {
                return Ok(await _courseService.GetAllCourses());
            }
            else
            {
                var serviceResponse = new ServiceResponse<NoParam>();
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_UNAUTHORIZED;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_UNAUTHORIZED;

                return BadRequest(serviceResponse);
            }
        }

        [HttpGet("GetCourseByID/{uid}")]
        public async Task<ActionResult<ServiceResponse<ResponseCourseDetails>>> GetCourseByID(string uid)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value == UserTypes.Admin.ToString())
            {
                return Ok(await _courseService.GetCourseByID(uid));
            }
            else
            {
                var serviceResponse = new ServiceResponse<NoParam>();
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_UNAUTHORIZED;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_UNAUTHORIZED;

                return BadRequest(serviceResponse);
            }
        }

        [HttpPost("AddCourse")]
        public async Task<ActionResult<ServiceResponse<ResponseCourseDetails>>> AddCourse(RequestAddCourse newCourse)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value == UserTypes.Admin.ToString())
            {
                return Ok(await _courseService.AddCourse(newCourse));
            }
            else
            {
                var serviceResponse = new ServiceResponse<NoParam>();
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_UNAUTHORIZED;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_UNAUTHORIZED;

                return BadRequest(serviceResponse);
            }
        }

        [HttpPut("UpdateCourse")]
        public async Task<ActionResult<ServiceResponse<ResponseCourseDetails>>> UpdateCourse(RequestUpdateCourse updateCourse)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value == UserTypes.Admin.ToString())
            {
                return Ok(await _courseService.UpdateCourse(updateCourse));
            }
            else
            {
                var serviceResponse = new ServiceResponse<NoParam>();
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_UNAUTHORIZED;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_UNAUTHORIZED;

                return BadRequest(serviceResponse);
            }
        }
    }
}