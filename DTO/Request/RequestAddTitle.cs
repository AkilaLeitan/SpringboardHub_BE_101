using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Request
{
    public class RequestAddTitle
    {
        public int SubjectID { get; set; } = AppConstants.DEFAULT;
        public string Name { get; set; } = string.Empty;
    }
}