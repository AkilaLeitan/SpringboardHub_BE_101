using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpringboardHub_BE_101.Service.SubjectService;

namespace SpringboardHub_BE_101.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : BaseController
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            this._subjectService = subjectService;
        }

        [HttpGet("GetAllSubjects")]
        public async Task<ActionResult<ServiceResponse<ICollection<ResponseSubjectDetails>>>> GetAllSubjects()
        {
            return Ok(await _subjectService.GetAllSubjects());
        }

        [HttpGet("GetSubjectByID/{uid}")]
        public async Task<ActionResult<ServiceResponse<ResponseSubjectDetails>>> GetSubjectByID(string uid)
        {
            return Ok(await _subjectService.GetSubjectByID(uid));
        }

        [HttpPost("AddSubject")]
        public async Task<ActionResult<ServiceResponse<ResponseSubjectDetails>>> AddSubject(RequestAddSubject newSubject)
        {
            return Ok(await _subjectService.AddSubject(newSubject));
        }

        [HttpPut("UpdateSubject")]
        public async Task<ActionResult<ServiceResponse<ResponseCourseDetails>>> UpdateSubject(RequestUpdateSubject updateSubject)
        {
            return Ok(await _subjectService.UpdateSubject(updateSubject));
        }
    }
}