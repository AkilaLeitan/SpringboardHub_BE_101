using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.SubjectService
{
    public class SubjectService : ISubjectService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public SubjectService(IMapper mapper, DataContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<ServiceResponse<ResponseSubjectDetails>> AddSubject(RequestAddSubject newSubject)
        {
            var serviceResponse = new ServiceResponse<ResponseSubjectDetails>();
            try
            {
                var subject = _mapper.Map<Subject>(newSubject);
                subject.UID = GenerateSubjectUID(subject.Credit);
                subject.CreatedDate = DateTime.Now;

                _context.Subject.Add(subject);
                await _context.SaveChangesAsync();

                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                serviceResponse.Payload = _mapper.Map<ResponseSubjectDetails>(_context.Subject.OrderByDescending(s => s.SubjectID).FirstOrDefault());
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ICollection<ResponseSubjectDetails>>> GetAllSubjects()
        {
            var serviceResponse = new ServiceResponse<ICollection<ResponseSubjectDetails>>();
            try
            {
                ICollection<Subject> dbSubject = await _context.Subject.ToListAsync();

                if (dbSubject is null || dbSubject.Count == AppConstants.DEFAULT)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = dbSubject.Select(s => _mapper.Map<ResponseSubjectDetails>(s)).ToList();
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseSubjectDetails>> GetSubjectByID(string uid)
        {
            var serviceResponse = new ServiceResponse<ResponseSubjectDetails>();
            try
            {
                var dbSubject = await _context.Subject.FirstOrDefaultAsync(s => s.UID == uid.ToLower());

                if (dbSubject is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = _mapper.Map<ResponseSubjectDetails>(dbSubject);
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseSubjectDetails>> UpdateSubject(RequestUpdateSubject updateSubject)
        {
            var serviceResponse = new ServiceResponse<ResponseSubjectDetails>();
            try
            {

                var targetSubject = await _context.Subject.FirstOrDefaultAsync(s => s.UID == updateSubject.UID.ToLower());

                if (targetSubject is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    targetSubject.Name = updateSubject.Name;
                    targetSubject.Description = updateSubject.Description;
                    targetSubject.Credit = updateSubject.Credit;
                    targetSubject.TextBook = updateSubject.TextBook;
                    targetSubject.ModifyDate = DateTime.Now;
                    await _context.SaveChangesAsync();

                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    var updatedSubject = await _context.Subject.FirstOrDefaultAsync(s => s.UID == updateSubject.UID.ToLower());
                    serviceResponse.Payload = _mapper.Map<ResponseSubjectDetails>(updatedSubject);
                }
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseSubjectSyllabus>> SubjectAddToSyllabus(RequestSubjectToSyllabus subjectToSyllabus)
        {
            var serviceResponse = new ServiceResponse<ResponseSubjectSyllabus>();
            try
            {
                if (!_context.Syllabus.Any(s => s.SyllabusID == subjectToSyllabus.SyllabusID))
                {
                    throw new Exception("Syllabus not found!");
                }
                else if (!_context.Subject.Any(s => s.SubjectID == subjectToSyllabus.SubjectID))
                {
                    throw new Exception("Subject not found!");
                }
                else if (!_context.Lecture.Any(l => l.LectureID == subjectToSyllabus.LectureID))
                {
                    throw new Exception("Lecture not found!");
                }
                else
                {
                    var subjectInSyllabus = _mapper.Map<SubjectInSyllabus>(subjectToSyllabus);
                    subjectInSyllabus.CreatedDate = DateTime.Now;

                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Enrollment ON");
                        _context.SubjectInSyllabus.Add(subjectInSyllabus);
                        await _context.SaveChangesAsync();
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Enrollment OFF");
                        transaction.Commit();
                    }

                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = _mapper.Map<ResponseSubjectSyllabus>(_context.SubjectInSyllabus.OrderByDescending(s => s.SubjectInSyllabusID).FirstOrDefault());
                }


            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public string GenerateSubjectUID(int credit)
        {
            int lastID = 1;

            if (_context.Course.Any())
            {
                lastID = _context.Subject
                .OrderByDescending(s => s.SubjectID)
                .Select(s => s.SubjectID)
                .FirstOrDefault() + 1;
            }

            return "com" + credit.ToString() + lastID.ToString();
        }


    }
}