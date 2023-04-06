using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CourseService(IMapper mapper, DataContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<ServiceResponse<ResponseCourseDetails>> AddCourse(RequestAddCourse newCourse)
        {
            var serviceResponse = new ServiceResponse<ResponseCourseDetails>();
            try
            {
                var course = _mapper.Map<Course>(newCourse);
                course.UID = GenerateCourseUID();
                course.CreatedDate = DateTime.Now;

                _context.Course.Add(course);
                await _context.SaveChangesAsync();

                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                serviceResponse.Payload = _mapper.Map<ResponseCourseDetails>(_context.Course.OrderByDescending(c => c.CourseID).FirstOrDefault());
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ICollection<ResponseCourseDetails>>> GetAllCourses()
        {
            var serviceResponse = new ServiceResponse<ICollection<ResponseCourseDetails>>();
            try
            {
                ICollection<Course> dbCourses = await _context.Course.ToListAsync();

                if (dbCourses is null || dbCourses.Count == AppConstants.DEFAULT)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = dbCourses.Select(c => _mapper.Map<ResponseCourseDetails>(c)).ToList();
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseCourseDetails>> GetCourseByID(string uid)
        {
            var serviceResponse = new ServiceResponse<ResponseCourseDetails>();
            try
            {
                var dbCourse = await _context.Course.FirstOrDefaultAsync(c => c.UID == uid.ToLower());

                if (dbCourse is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = _mapper.Map<ResponseCourseDetails>(dbCourse);
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseCourseDetails>> UpdateCourse(RequestUpdateCourse updateCourse)
        {
            var serviceResponse = new ServiceResponse<ResponseCourseDetails>();
            try
            {

                var targetCourse = await _context.Course.FirstOrDefaultAsync(c => c.UID == updateCourse.UID.ToLower());

                if (targetCourse is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    targetCourse.CourseName = updateCourse.CourseName;
                    targetCourse.Description = updateCourse.Description;
                    targetCourse.Duration = updateCourse.Duration;
                    targetCourse.ModifyDate = DateTime.Now;
                    await _context.SaveChangesAsync();

                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    var updatedCourse = await _context.Course.FirstOrDefaultAsync(c => c.UID == updateCourse.UID.ToLower());
                    serviceResponse.Payload = _mapper.Map<ResponseCourseDetails>(updatedCourse);
                }
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public string GenerateCourseUID()
        {
            int lastID = 1;

            if (_context.Course.Any())
            {
                lastID = _context.Course
                .OrderByDescending(c => c.CourseID)
                .Select(c => c.CourseID)
                .FirstOrDefault() + 1;
            }

            return "course" + lastID.ToString();
        }
    }
}