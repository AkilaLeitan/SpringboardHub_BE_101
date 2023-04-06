using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpringboardHub_BE_101.Service.EnrollmentService;

namespace SpringboardHub_BE_101.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollemntController : BaseController
    {
        private readonly IEnrollmentService _enrollementService;

        public EnrollemntController(IEnrollmentService enrollementService)
        {
            this._enrollementService = enrollementService;
        }

        [HttpGet("GetAllEnrollements")]
        public async Task<ActionResult<ServiceResponse<ICollection<ResponseEnrollementDetails>>>> GetAllEnrollements()
        {
            return Ok(await _enrollementService.GetAllEnrollemnts());
        }

        [HttpGet("GetEnrollmentByID/{id}")]
        public async Task<ActionResult<ServiceResponse<ResponseEnrollementDetails>>> GetEnrollmentByID(int id)
        {
            return Ok(await _enrollementService.GetEnrollementByID(id));
        }

        [HttpPost("AddEnrollement")]
        public async Task<ActionResult<ServiceResponse<ResponseEnrollementDetails>>> AddEnrollement(RequestAddEnrollemnt newEnrollement)
        {
            return Ok(await _enrollementService.AddEnrollment(newEnrollement));
        }

        // [HttpPut("UpdateEnrollement")]
        // public async Task<ActionResult<ServiceResponse<ResponseEnrollementDetails>>> UpdateEnrollement(RequestUpdateEnrollement updateEnrollement)
        // {
        //     return Ok(await _enrollementService.UpdateEnrollement(updateEnrollement));
        // }
    }
}