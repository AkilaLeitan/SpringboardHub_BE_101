using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Model
{
    public class Enrollment : DbBaseClass
    {
        public int EnrollmentID { get; set; } = AppConstants.DEFAULT;
        public Course? Course { get; set; }
        public Batch? Batch { get; set; }
        public Syllabus? Syllabus { get; set; }
    }
}