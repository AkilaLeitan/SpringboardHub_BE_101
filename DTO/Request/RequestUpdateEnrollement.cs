using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Request
{
    public class RequestUpdateEnrollement
    {
        public int EnrollmentsID { get; set; } = AppConstants.DEFAULT;
        public int CourseID { get; set; } = AppConstants.DEFAULT;
        public int BatchID { get; set; } = AppConstants.DEFAULT;
    }
}