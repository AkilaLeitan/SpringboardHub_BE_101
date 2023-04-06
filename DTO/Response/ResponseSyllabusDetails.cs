using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Response
{
    public class ResponseSyllabusDetails
    {
        public int SyllabusID { get; set; } = AppConstants.DEFAULT;
        public string UID { get; set; } = AppConstants.DEFAULT_ID;
        public string Name { get; set; } = string.Empty;
        public int EnrollmentID { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}