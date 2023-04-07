using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Response
{
    public class ResponseStudentArticle
    {
        public string? BatchName { get; set; }
        public string? CourseName { get; set; }
        public int? SyllabusID { get; set; }
        public ICollection<ResponseSubjectLecture>? SubjectList { get; set; }
    }

    public class ResponseSubjectLecture
    {
        public string? SubjectName { get; set; }
        public string? LectureName { get; set; }
        public ICollection<ResponseTitleArticle>? TitleList { get; set; }
    }

    public class ResponseTitleArticle
    {
        public string? Title { get; set; }
        public ICollection<ResponseArticleDetails>? ArticleList { get; set; }
    }
}