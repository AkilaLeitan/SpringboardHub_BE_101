using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.BatchService
{
    public interface IBatchService
    {
        Task<ServiceResponse<ICollection<ResponseBatchDetails>>> GetAllBatches();
        Task<ServiceResponse<ResponseBatchDetails>> GetBatchByID(string uid);
        Task<ServiceResponse<ResponseBatchDetails>> AddBatch(RequestAddBatch newBatch);
        Task<ServiceResponse<ResponseBatchDetails>> UpdateBatch(RequestUpdateBatch updateBatch);
    }
}