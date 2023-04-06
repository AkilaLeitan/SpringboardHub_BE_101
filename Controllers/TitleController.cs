using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpringboardHub_BE_101.Service.TitleService;

namespace SpringboardHub_BE_101.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TitleController : BaseController
    {
        private readonly ITitleService _titleService;

        public TitleController(ITitleService titleService)
        {
            this._titleService = titleService;
        }

        [HttpGet("GetAllTitles")]
        public async Task<ActionResult<ServiceResponse<ICollection<ResponseTitleDetails>>>> GetAllTitles()
        {
            return Ok(await _titleService.GetAllTitle());
        }

        [HttpGet("GetAllTitlesBySubject")]
        public async Task<ActionResult<ServiceResponse<ICollection<ResponseTitleDetails>>>> GetAllTitlesBySubject(string uid)
        {
            return Ok(await _titleService.GetTitleBySubject(uid));
        }

        [HttpGet("GetTitleByID/{uid}")]
        public async Task<ActionResult<ServiceResponse<ResponseTitleDetails>>> GetTitleByID(string uid)
        {
            return Ok(await _titleService.GetTitleByID(uid));
        }

        [HttpPost("AddTitle")]
        public async Task<ActionResult<ServiceResponse<ResponseTitleDetails>>> AddTitle(RequestAddTitle newTitle)
        {
            return Ok(await _titleService.AddTitle(newTitle));
        }

        [HttpPut("UpdateTitle")]
        public async Task<ActionResult<ServiceResponse<ResponseTitleDetails>>> UpdateTitle(RequestUpdateTitle updateTitle)
        {
            return Ok(await _titleService.updateTitle(updateTitle));
        }
    }
}