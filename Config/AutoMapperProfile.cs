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
            CreateMap<RequestAddBatch, Batch>();
            CreateMap<RequestAddCourse, Course>();
        }
    }
}