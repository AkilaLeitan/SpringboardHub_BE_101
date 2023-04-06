using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Model
{
    public class Student : User
    {
        public int StudentID { get; set; } = AppConstants.DEFAULT;
        public string UID { get; set; } = AppConstants.DEFAULT_ID;
        public string Guardian { get; set; } = string.Empty;
        public string JoinedYear { get; set; } = AppConstants.DEFAULT_ID;
        public DateTime DOB { get; set; } = DateTime.MinValue;
        public Batch? Batch { get; set; }
        public int BatchID { get; set; } = AppConstants.DEFAULT;
    }
}