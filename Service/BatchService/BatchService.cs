using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.BatchService
{
    public class BatchService : IBatchService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public BatchService(IMapper mapper, DataContext context)
        {
            this._mapper = mapper;
            this._context = context;

        }

        public async Task<ServiceResponse<ResponseBatchDetails>> AddBatch(RequestAddBatch newBatch)
        {
            var serviceResponse = new ServiceResponse<ResponseBatchDetails>();
            try
            {
                var batch = _mapper.Map<Batch>(newBatch);
                batch.UID = GenerateBatchUID();
                batch.CreatedDate = DateTime.Now;

                _context.Batch.Add(batch);
                await _context.SaveChangesAsync();

                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                serviceResponse.Payload = _mapper.Map<ResponseBatchDetails>(_context.Batch.OrderByDescending(b => b.BatchID).FirstOrDefault());
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ICollection<ResponseBatchDetails>>> GetAllBatches()
        {
            var serviceResponse = new ServiceResponse<ICollection<ResponseBatchDetails>>();
            try
            {
                ICollection<Batch> dbBatches = await _context.Batch.ToListAsync();

                if (dbBatches is null || dbBatches.Count == AppConstants.DEFAULT)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = dbBatches.Select(b => _mapper.Map<ResponseBatchDetails>(b)).ToList();
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseBatchDetails>> GetBatchByID(string uid)
        {
            var serviceResponse = new ServiceResponse<ResponseBatchDetails>();
            try
            {
                var dbBatches = await _context.Batch.FirstOrDefaultAsync(b => b.UID == uid.ToLower());

                if (dbBatches is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = _mapper.Map<ResponseBatchDetails>(dbBatches);
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseBatchDetails>> UpdateBatch(RequestUpdateBatch updateBatch)
        {
            var serviceResponse = new ServiceResponse<ResponseBatchDetails>();
            try
            {

                var targetBatch = await _context.Batch.FirstOrDefaultAsync(b => b.UID == updateBatch.UID.ToLower());

                if (targetBatch is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    targetBatch.BatchName = updateBatch.BatchName;
                    targetBatch.Description = updateBatch.Description;
                    targetBatch.ModifyDate = DateTime.Now;
                    await _context.SaveChangesAsync();

                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    var updatedBatch = await _context.Batch.FirstOrDefaultAsync(b => b.UID == updateBatch.UID.ToLower());
                    serviceResponse.Payload = _mapper.Map<ResponseBatchDetails>(updatedBatch);
                }
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public string GenerateBatchUID()
        {

            int year = DateTime.Now.Year;
            int lastID = 1;

            if (_context.Course.Any())
            {
                lastID = _context.Batch
                .OrderByDescending(b => b.BatchID)
                .Select(b => b.BatchID)
                .FirstOrDefault() + 1;
            }

            return year.ToString() + "calceysb" + lastID.ToString();
        }
    }
}