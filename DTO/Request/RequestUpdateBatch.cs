using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Request
{
    public class RequestUpdateBatch
    {
        public string UID { get; set; } = AppConstants.DEFAULT_ID;
        public string BatchName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}