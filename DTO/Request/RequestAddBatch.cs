using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Request
{
    public class RequestAddBatch
    {
        public string BatchName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartedDate = DateTime.MinValue;
    }
}