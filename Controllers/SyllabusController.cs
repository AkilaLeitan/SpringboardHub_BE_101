using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpringboardHub_BE_101.Service.SyllabusService;

namespace SpringboardHub_BE_101.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SyllabusController : BaseController
    {
        private readonly ISyllabusService _syllabusService;

        public SyllabusController(ISyllabusService syllabusService)
        {
            this._syllabusService = syllabusService;
        }

        [HttpGet("GetAllSyllabus")]
        public async Task<ActionResult<ServiceResponse<ICollection<ResponseSyllabusDetails>>>> GetAllSyllabus()
        {
            return Ok(await _syllabusService.GetAllSyllabus());
        }

        [HttpGet("GetSyllabusByID/{uid}")]
        public async Task<ActionResult<ServiceResponse<ResponseSyllabusDetails>>> GetSyllabusByID(string uid)
        {
            return Ok(await _syllabusService.GetSyllabusByID(uid));
        }

        [HttpPost("AddSyllabus")]
        public async Task<ActionResult<ServiceResponse<ResponseSyllabusDetails>>> AddSyllabus(RequestAddSyllabus newSyllabus)
        {
            return Ok(await _syllabusService.AddSyllabus(newSyllabus));
        }

        [HttpPut("UpdateSyllabus")]
        public async Task<ActionResult<ServiceResponse<ResponseSyllabusDetails>>> UpdateSyllabus(RequestUpdateSyllabus updateSyllabus)
        {
            return Ok(await _syllabusService.UpdateSyllabus(updateSyllabus));
        }
    }
}