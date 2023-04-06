using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.EnrollmentService
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public EnrollmentService(IMapper mapper, DataContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<ServiceResponse<ResponseEnrollementDetails>> AddEnrollment(RequestAddEnrollemnt newEnrollement)
        {
            var serviceResponse = new ServiceResponse<ResponseEnrollementDetails>();

            try
            {
                var batch = await _context.Batch.FirstOrDefaultAsync(b => b.BatchID == newEnrollement.BatchID);
                var course = await _context.Course.FirstOrDefaultAsync(c => c.CourseID == newEnrollement.CourseID);

                if (batch is null || course is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else if (_context.Enrollment.Any(e => e.BatchID == newEnrollement.BatchID && e.CourseID == newEnrollement.CourseID))
                {
                    throw new Exception("This batch and course already has enrollment");
                }
                else
                {
                    var enrollemnet = new Enrollment { Batch = batch, Course = course };
                    enrollemnet.BatchID = batch.BatchID;
                    enrollemnet.CourseID = course.CourseID;
                    enrollemnet.EnrollmentID = GenerateEnrollementID();
                    enrollemnet.CreatedDate = DateTime.Now;
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Enrollment ON");
                        _context.Enrollment.Add(enrollemnet);
                        await _context.SaveChangesAsync();
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Enrollment OFF");
                        transaction.Commit();
                    }

                    await _context.SaveChangesAsync();

                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = _mapper.Map<ResponseEnrollementDetails>(_context.Enrollment.OrderByDescending(e => e.EnrollmentID).FirstOrDefault());
                }
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<ICollection<ResponseEnrollementDetails>>> GetAllEnrollemnts()
        {
            var serviceResponse = new ServiceResponse<ICollection<ResponseEnrollementDetails>>();
            try
            {
                ICollection<Enrollment> dbEnrollements = await _context.Enrollment.ToListAsync();

                if (dbEnrollements is null || dbEnrollements.Count == AppConstants.DEFAULT)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = dbEnrollements.Select(s => _mapper.Map<ResponseEnrollementDetails>(s)).ToList();
                }
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseEnrollementDetails>> GetEnrollementByID(int id)
        {
            var serviceResponse = new ServiceResponse<ResponseEnrollementDetails>();
            try
            {
                var dbEnrollement = await _context.Enrollment.FirstOrDefaultAsync(e => e.EnrollmentID == id);

                if (dbEnrollement is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = _mapper.Map<ResponseEnrollementDetails>(dbEnrollement);
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public Task<ServiceResponse<ResponseEnrollementDetails>> UpdateEnrollement(RequestUpdateEnrollement updateEnrollement)
        {
            throw new NotImplementedException();
            ///TODO
        }

        private int GenerateEnrollementID()
        {
            int lastID = 1;

            if (_context.Enrollment.Any())
            {
                lastID = _context.Enrollment
                .OrderByDescending(e => e.EnrollmentID)
                .Select(e => e.EnrollmentID)
                .FirstOrDefault() + 1;
            }

            return lastID;
        }

    }
}