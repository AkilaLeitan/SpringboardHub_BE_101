using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Model
{
    public class SubjectInSyllabus : DbBaseClass
    {
        public int SubjectInSyllabusID { get; set; } = AppConstants.DEFAULT;
        public Syllabus? Syllabus { get; set; }
        public int SyllabusID { get; set; } = AppConstants.DEFAULT;
        public Subject? Subject { get; set; }
        public int SubjectID { get; set; } = AppConstants.DEFAULT;
        public Lecture? Lecture { get; set; }
        public int LectureID { get; set; } = AppConstants.DEFAULT;
    }
}