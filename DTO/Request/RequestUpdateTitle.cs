using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Request
{
    public class RequestUpdateTitle
    {
        public string UID { get; set; } = AppConstants.DEFAULT_ID;
        public int SubjectID { get; set; } = AppConstants.DEFAULT;
        public string Name { get; set; } = string.Empty;
    }
}