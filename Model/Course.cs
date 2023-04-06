using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Model
{
    public class Course : DbBaseClass
    {
        public int CourseID { get; set; } = AppConstants.DEFAULT;
        public string UID { get; set; } = AppConstants.DEFAULT_ID;
        public string CourseName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Duration { get; set; } = string.Empty;
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}