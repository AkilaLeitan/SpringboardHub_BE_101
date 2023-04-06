using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Model
{
    public class Subject : DbBaseClass
    {
        public int SubjectID { get; set; } = AppConstants.DEFAULT;
        public string UID { get; set; } = AppConstants.DEFAULT_ID;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Credit { get; set; } = 0;
        public string? TextBook { get; set; }
    }
}