using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Request
{
    public class RequestAddSyllabus
    {
        public string Name { get; set; } = string.Empty;
        public int? EnrollmentID { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}