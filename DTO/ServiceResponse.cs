using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO
{
    public class ServiceResponse<T>
    {
        public T? Payload { get; set; }
        public string ResponseCode { get; set; } = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
        public string ResponseMessage { get; set; } = string.Empty;
    }
}