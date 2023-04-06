using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.CourseService
{
    public interface ICourseService
    {
        Task<ServiceResponse<ICollection<ResponseCourseDetails>>> GetAllCourses();
        Task<ServiceResponse<ResponseCourseDetails>> GetCourseByID(string uid);
        Task<ServiceResponse<ResponseCourseDetails>> AddCourse(RequestAddCourse newCourse);
        Task<ServiceResponse<ResponseCourseDetails>> UpdateCourse(RequestUpdateCourse updateCourse);
    }
}