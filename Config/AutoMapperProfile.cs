using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Config
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Batch, ResponseBatchDetails>();
            CreateMap<Admin, ResponseUserAdminDetails>();
            CreateMap<Course, ResponseCourseDetails>();
            CreateMap<Student, ResponseUserStudentDetails>();
            CreateMap<Enrollment, ResponseEnrollementDetails>().ForMember(e => e.EnrolledDate, o => o.MapFrom(s => s.CreatedDate));
            CreateMap<Syllabus, ResponseSyllabusDetails>();
            CreateMap<Subject, ResponseSubjectDetails>();
            CreateMap<Lecture, ResponseUserLectureDetails>();
            CreateMap<Title, ResponseTitleDetails>();
            CreateMap<RequestAddBatch, Batch>();
            CreateMap<RequestAddCourse, Course>();
            CreateMap<RequestAddEnrollemnt, Enrollment>();
            CreateMap<RequestAddSyllabus, Syllabus>();
            CreateMap<RequestAddSubject, Subject>();
            CreateMap<RequestAddTitle, Title>();
        }
    }
}