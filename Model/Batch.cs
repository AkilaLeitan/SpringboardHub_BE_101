using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Model
{
    public class Batch : DbBaseClass
    {
        public int BatchID { get; set; } = AppConstants.DEFAULT;
        public string UID { get; set; } = AppConstants.DEFAULT_ID;
        public string BatchName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartedDate { get; set; } = DateTime.MinValue;
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}