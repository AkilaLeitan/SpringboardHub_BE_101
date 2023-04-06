using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public StudentService(IMapper mapper, DataContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<ServiceResponse<NoParam>> DeleteStudent(string uid)
        {
            var serviceResponse = new ServiceResponse<NoParam>();
            var student = await _context.Student.FirstOrDefaultAsync(s => s.UID == uid.ToLower());

            if (student is null)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
            }
            else
            {
                _context.Student.Remove(student);
                await _context.SaveChangesAsync();

                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ICollection<ResponseUserStudentDetails>>>> GetAllStudents()
        {
            var serviceResponse = new ServiceResponse<List<ICollection<ResponseUserStudentDetails>>>();

            ICollection<Batch> batchList = await _context.Batch.ToListAsync();
            List<ICollection<ResponseUserStudentDetails>> studentsList = new List<ICollection<ResponseUserStudentDetails>>();

            if (batchList is null)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
            }
            else
            {
                foreach (Batch batch in batchList)
                {
                    ICollection<Student> students = await _context.Student
                    .Where(s => s.Batch!.BatchID == batch.BatchID)
                    .OrderBy(s => s.StudentID)
                    .ToListAsync();

                    studentsList.Add(_mapper.Map<List<ResponseUserStudentDetails>>(students));
                }
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                serviceResponse.Payload = studentsList;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseUserStudentDetails>> GetStudentByID(string uid)
        {
            var serviceResponse = new ServiceResponse<ResponseUserStudentDetails>();
            try
            {
                var dbStudent = await _context.Student.FirstOrDefaultAsync(s => s.UID == uid.ToLower());

                if (dbStudent is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    ResponseUserStudentDetails responseUserStudentDetails = _mapper.Map<ResponseUserStudentDetails>(dbStudent);
                    responseUserStudentDetails.BatchUID = _context.Batch.FirstOrDefault(b => b.BatchID == dbStudent.BatchID)!.UID;
                    serviceResponse.Payload = responseUserStudentDetails;
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ICollection<ResponseUserStudentDetails>>> GetStudentsByBatch(string batchUID)
        {
            var serviceResponse = new ServiceResponse<ICollection<ResponseUserStudentDetails>>();

            try
            {
                var batchRecode = _context.Batch.FirstOrDefault(b => b.UID == batchUID.ToLower());


                if (batchRecode is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    ICollection<Student> students = await _context.Student.Where(s => s.Batch!.UID == batchUID.ToLower()).ToListAsync();

                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = _mapper.Map<ICollection<ResponseUserStudentDetails>>(students);
                }
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseUserStudentDetails>> UpdateStudent(RequestUpdateUserStudent updateStudent)
        {
            var serviceResponse = new ServiceResponse<ResponseUserStudentDetails>();
            try
            {

                var targetStudent = await _context.Student.FirstOrDefaultAsync(s => s.UID == updateStudent.UID.ToLower());


                if (targetStudent is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    var batchRecode = _context.Batch.FirstOrDefault(b => b.UID == updateStudent.BatchUID.ToLower());
                    if (batchRecode is null)
                    {
                        throw new Exception("Batch is not Found");
                    }
                    else
                    {
                        targetStudent.FirstName = updateStudent.FirstName;
                        targetStudent.LastName = updateStudent.LastName;
                        targetStudent.Email = updateStudent.Email;
                        targetStudent.Telephone = updateStudent.Telephone;
                        targetStudent.NIC = updateStudent.NIC;
                        targetStudent.Guardian = updateStudent.Guardian;
                        targetStudent.JoinedYear = updateStudent.JoinedYear;
                        targetStudent.DOB = updateStudent.DOB;
                        targetStudent.Batch = batchRecode;
                        targetStudent.ModifyDate = DateTime.Now;
                        await _context.SaveChangesAsync();

                        serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                        serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                        var updatedStudent = await _context.Student.FirstOrDefaultAsync(s => s.UID == updateStudent.UID.ToLower());
                        serviceResponse.Payload = _mapper.Map<ResponseUserStudentDetails>(updatedStudent);
                    }

                }
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }
    }
}