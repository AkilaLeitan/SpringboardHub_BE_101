using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.SyllabusService
{
    public class SyllabusService : ISyllabusService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public SyllabusService(IMapper mapper, DataContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }
        public async Task<ServiceResponse<ResponseSyllabusDetails>> AddSyllabus(RequestAddSyllabus newSyllabus)
        {
            var serviceResponse = new ServiceResponse<ResponseSyllabusDetails>();
            try
            {
                var targetEnrollemnt = await _context.Enrollment.FirstOrDefaultAsync(e => e.EnrollmentID == newSyllabus.EnrollmentID);
                if (targetEnrollemnt is null)
                {
                    throw new Exception("Enrollement ID is not found");
                }
                else if (await _context.Syllabus.AnyAsync(s => s.Enrollment == targetEnrollemnt))
                {
                    throw new Exception("This Enrollement already has a syllabus");
                }
                var syllabus = new Syllabus();
                syllabus.Name = newSyllabus.Name;
                syllabus.Enrollment = targetEnrollemnt;
                syllabus.CreatedBy = newSyllabus.CreatedBy;
                syllabus.UID = GenerateSyllabusID();
                syllabus.CreatedDate = DateTime.Now;

                _context.Syllabus.Add(syllabus);
                await _context.SaveChangesAsync();
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                serviceResponse.Payload = _mapper.Map<ResponseSyllabusDetails>(_context.Syllabus.OrderByDescending(s => s.SyllabusID).FirstOrDefault());
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ICollection<ResponseSyllabusDetails>>> GetAllSyllabus()
        {
            var serviceResponse = new ServiceResponse<ICollection<ResponseSyllabusDetails>>();
            try
            {
                ICollection<Syllabus> dbSyllabus = await _context.Syllabus.ToListAsync();

                if (dbSyllabus is null || dbSyllabus.Count == AppConstants.DEFAULT)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = dbSyllabus.Select(s => _mapper.Map<ResponseSyllabusDetails>(s)).ToList();
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseSyllabusDetails>> GetSyllabusByID(string uid)
        {
            var serviceResponse = new ServiceResponse<ResponseSyllabusDetails>();
            try
            {
                var dbSyllabus = await _context.Syllabus.FirstOrDefaultAsync(s => s.UID == uid.ToLower());

                if (dbSyllabus is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = _mapper.Map<ResponseSyllabusDetails>(dbSyllabus);
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseSyllabusDetails>> UpdateSyllabus(RequestUpdateSyllabus updateSyllabus)
        {
            var serviceResponse = new ServiceResponse<ResponseSyllabusDetails>();
            try
            {

                var targetSyllabus = await _context.Syllabus.FirstOrDefaultAsync(s => s.UID == updateSyllabus.UID.ToLower());

                if (targetSyllabus is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    targetSyllabus.Name = updateSyllabus.Name;
                    targetSyllabus.Enrollment = await _context.Enrollment.FirstOrDefaultAsync(e => e.EnrollmentID == updateSyllabus.EnrollmentID);
                    targetSyllabus.CreatedBy = updateSyllabus.CreatedBy;
                    targetSyllabus.ModifyDate = DateTime.Now;
                    await _context.SaveChangesAsync();

                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    var updatedSyllabus = await _context.Syllabus.FirstOrDefaultAsync(s => s.UID == updateSyllabus.UID.ToLower());
                    serviceResponse.Payload = _mapper.Map<ResponseSyllabusDetails>(updatedSyllabus);
                }
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public string GenerateSyllabusID()
        {
            int lastID = 1;

            if (_context.Syllabus.Any())
            {
                lastID = _context.Syllabus
                .OrderByDescending(c => c.SyllabusID)
                .Select(c => c.SyllabusID)
                .FirstOrDefault() + 1;
            }

            return "sl" + lastID.ToString();
        }
    }
}