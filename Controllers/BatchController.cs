using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SpringboardHub_BE_101.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BatchController : BaseController
    {
        private readonly IBatchService _batchService;

        public BatchController(IBatchService batchService)
        {
            this._batchService = batchService;
        }

        [HttpGet("GetAllBatches")]
        public async Task<ActionResult<ServiceResponse<ICollection<ResponseBatchDetails>>>> GetAllBatches()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value == UserTypes.Admin.ToString())
            {
                return Ok(await _batchService.GetAllBatches());
            }
            else
            {
                var serviceResponse = new ServiceResponse<NoParam>();
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_UNAUTHORIZED;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_UNAUTHORIZED;

                return BadRequest(serviceResponse);
            }
        }

        [HttpGet("GetBatchByID/{uid}")]
        public async Task<ActionResult<ServiceResponse<ResponseBatchDetails>>> GetBatchByID(string uid)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value == UserTypes.Admin.ToString())
            {
                return Ok(await _batchService.GetBatchByID(uid));
            }
            else
            {
                var serviceResponse = new ServiceResponse<NoParam>();
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_UNAUTHORIZED;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_UNAUTHORIZED;

                return BadRequest(serviceResponse);
            }
        }

        [HttpPost("AddBatch")]
        public async Task<ActionResult<ServiceResponse<ResponseBatchDetails>>> AddBatch(RequestAddBatch newBatch)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value == UserTypes.Admin.ToString())
            {
                return Ok(await _batchService.AddBatch(newBatch));
            }
            else
            {
                var serviceResponse = new ServiceResponse<NoParam>();
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_UNAUTHORIZED;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_UNAUTHORIZED;

                return BadRequest(serviceResponse);
            }
        }

        [HttpPut("UpdateBatch")]
        public async Task<ActionResult<ServiceResponse<ResponseBatchDetails>>> UpdateBatch(RequestUpdateBatch updateBatch)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value == UserTypes.Admin.ToString())
            {
                return Ok(await _batchService.UpdateBatch(updateBatch));
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