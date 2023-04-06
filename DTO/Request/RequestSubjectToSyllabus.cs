using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Request
{
    public class RequestSubjectToSyllabus
    {
        public int SyllabusID { get; set; } = AppConstants.DEFAULT;
        public int SubjectID { get; set; } = AppConstants.DEFAULT;
        public int LectureID { get; set; } = AppConstants.DEFAULT;
    }
}