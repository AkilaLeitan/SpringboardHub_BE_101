using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Response
{
    public class ResponseEnrollementDetails
    {
        public int EnrollmentID { get; set; } = AppConstants.DEFAULT;
        public int CourseID { get; set; } = AppConstants.DEFAULT;
        public int BatchID { get; set; } = AppConstants.DEFAULT;
        public DateTime EnrolledDate { get; set; }
    }
}