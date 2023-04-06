using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace SpringboardHub_BE_101.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthRepository(IMapper mapper, DataContext context, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._context = context;
            this._config = configuration;
        }

        public async Task<ServiceResponse<ResponseUserAdminDetails>> AdminRegister(Admin newAdmin, string password)
        {
            var serviceResponse = new ServiceResponse<ResponseUserAdminDetails>();

            try
            {
                newAdmin.Password = GeneratePassword(password);
                newAdmin.UID = GenerateAdminUID();
                newAdmin.UserName = newAdmin.UID;
                newAdmin.UserType = UserTypes.Admin;
                newAdmin.CreatedDate = DateTime.Now;

                //Admin User Validations
                if (!AppFunctions.EmailValidater(newAdmin.Email))
                {
                    throw new Exception("Email not valid");
                }

                if (!AppFunctions.TelephoneValidater(newAdmin.Telephone))
                {
                    throw new Exception("Telephone number not valid");
                }

                if (AppFunctions.StringNullOrEmpty(newAdmin.FirstName) || AppFunctions.StringNullOrEmpty(newAdmin.LastName))
                {
                    throw new Exception("name cannot be null or empty");
                }

                _context.Admin.Add(newAdmin);
                await _context.SaveChangesAsync();

                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                serviceResponse.Payload = _mapper.Map<ResponseUserAdminDetails>(_context.Admin.OrderByDescending(a => a.AdminID).FirstOrDefault());

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseUserStudentDetails>> StudentRegister(RequestAddUserStudent newStudent, string password)
        {
            var serviceResponse = new ServiceResponse<ResponseUserStudentDetails>();

            try
            {
                var student = new Student();
                var batchRecode = _context.Batch.Find(newStudent.BatchID);
                if (batchRecode is null)
                {
                    throw new Exception("Batch is not Found");
                }
                else
                {
                    student.Password = GeneratePassword(password);
                    student.UID = GenerateStudentUID(batchRecode.UID, newStudent.JoinedYear);
                    student.UserName = student.UID;
                    student.UserType = UserTypes.Student;
                    student.CreatedDate = DateTime.Now;
                    student.Batch = batchRecode;
                    student.FirstName = newStudent.FirstName;
                    student.LastName = newStudent.LastName;
                    student.Telephone = newStudent.Telephone;
                    student.Email = newStudent.Email;
                    student.NIC = newStudent.NIC;
                    student.Guardian = newStudent.Guardian;
                    student.DOB = newStudent.DOB;
                    student.JoinedYear = newStudent.JoinedYear;
                }

                //Student User Validations
                if (!AppFunctions.EmailValidater(student.Email))
                {
                    throw new Exception("Email not valid");
                }

                if (!AppFunctions.TelephoneValidater(student.Telephone))
                {
                    throw new Exception("Telephone number not valid");
                }

                if (AppFunctions.StringNullOrEmpty(student.FirstName) || AppFunctions.StringNullOrEmpty(student.LastName))
                {
                    throw new Exception("name cannot be null or empty");
                }

                _context.Student.Add(student);
                await _context.SaveChangesAsync();

                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                serviceResponse.Payload = _mapper.Map<ResponseUserStudentDetails>(_context.Student.OrderByDescending(s => s.StudentID).FirstOrDefault());

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseUserLectureDetails>> LectureRegister(Lecture newLecture, string password)
        {
            var serviceResponse = new ServiceResponse<ResponseUserLectureDetails>();

            try
            {
                newLecture.Password = GeneratePassword(password);
                newLecture.UID = GenerateLectureUID();
                newLecture.UserName = newLecture.UID;
                newLecture.UserType = UserTypes.Lecture;
                newLecture.CreatedDate = DateTime.Now;

                //Lecture User Validations
                if (!AppFunctions.EmailValidater(newLecture.Email))
                {
                    throw new Exception("Email not valid");
                }

                if (!AppFunctions.TelephoneValidater(newLecture.Telephone))
                {
                    throw new Exception("Telephone number not valid");
                }

                if (AppFunctions.StringNullOrEmpty(newLecture.FirstName) || AppFunctions.StringNullOrEmpty(newLecture.LastName))
                {
                    throw new Exception("name cannot be null or empty");
                }

                _context.Lecture.Add(newLecture);
                await _context.SaveChangesAsync();

                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                serviceResponse.Payload = _mapper.Map<ResponseUserLectureDetails>(_context.Lecture.OrderByDescending(s => s.LectureID).FirstOrDefault());

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseAuth>> Login(RequestUserLogin loginUser)
        {
            var serviceResponse = new ServiceResponse<ResponseAuth>();

            //Admin User Login
            if (loginUser.UserType == UserTypes.Admin)
            {
                var user = await _context.Admin.FirstOrDefaultAsync(a => a.UserName.ToLower() == loginUser.UserName.ToLower());

                if (user is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else if (!user.Password.SequenceEqual(GeneratePassword(loginUser.Password)))
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_LOGINFAILED;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = new ResponseAuth { ID = user.AdminID, UID = user.UID, Token = CreateTokenForAdmin(user) };
                }
            }
            else if (loginUser.UserType == UserTypes.Lecture)
            {
                //
            }
            else if (loginUser.UserType == UserTypes.Student)
            {
                //
            }
            else
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_UNAUTHORIZED;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_UNAUTHORIZED;
            }

            return serviceResponse;
        }

        private string CreateTokenForAdmin(User user)
        {
            var clams = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.Role , user.UserType.ToString()!)
            };

            var appSettingsToken = _config.GetSection("AppSettings:IronMan").Value;

            if (appSettingsToken is null)
            {
                throw new Exception("App Settings token is null");
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(clams),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> UserExists(UserTypes userType, string userName)
        {
            if (userType == UserTypes.Admin)
            {
                if (await _context.Admin.AnyAsync(a => a.UserName.ToLower() == userName.ToLower()))
                {
                    return true;
                }
            }
            else if (userType == UserTypes.Lecture)
            {
                ///TODO
            }
            else if (userType == UserTypes.Student)
            {
                ///TODO
            }

            return false;
        }

        public string GenerateAdminUID()
        {
            int lastID = 1;

            if (_context.Admin.Any())
            {
                lastID = _context.Admin
                .OrderByDescending(a => a.AdminID)
                .Select(a => a.AdminID)
                .FirstOrDefault() + 1;
            }

            return "calcey_admin" + lastID.ToString();
        }

        public string GenerateStudentUID(string batchUID, string joinYear)
        {
            int lastID = 1;

            if (_context.Student.Any())
            {
                lastID = _context.Student
                .OrderByDescending(s => s.StudentID)
                .Select(s => s.StudentID)
                .FirstOrDefault() + 1;
            }

            return joinYear + "/" + batchUID.ToLower() + "/" + lastID.ToString();
        }

        public string GenerateLectureUID()
        {
            int lastID = 1;

            if (_context.Lecture.Any())
            {
                lastID = _context.Lecture
                .OrderByDescending(l => l.LectureID)
                .Select(l => l.LectureID)
                .FirstOrDefault() + 1;
            }

            return "calcey_lecture" + lastID.ToString();
        }

        private byte[] GeneratePassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return hash;
            }
        }

    }
}