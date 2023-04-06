using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.TitleService
{
    public class TitleService : ITitleService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;


        public TitleService(IMapper mapper, DataContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<ServiceResponse<ResponseTitleDetails>> AddTitle(RequestAddTitle newTitle)
        {
            var serviceResponse = new ServiceResponse<ResponseTitleDetails>();
            try
            {
                var title = _mapper.Map<Title>(newTitle);
                title.UID = GenerateTitleUID(title.SubjectID);
                title.CreatedDate = DateTime.Now;

                _context.Title.Add(title);
                await _context.SaveChangesAsync();

                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                serviceResponse.Payload = _mapper.Map<ResponseTitleDetails>(_context.Title.OrderByDescending(t => t.TitleID).FirstOrDefault());
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ICollection<ResponseTitleDetails>>> GetAllTitle()
        {
            var serviceResponse = new ServiceResponse<ICollection<ResponseTitleDetails>>();
            try
            {
                ICollection<Title> dbTitle = await _context.Title.ToListAsync();

                if (dbTitle is null || dbTitle.Count == AppConstants.DEFAULT)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = dbTitle.Select(t => _mapper.Map<ResponseTitleDetails>(t)).ToList();
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ICollection<ResponseTitleDetails>>> GetTitleBySubject(string subjectUID)
        {
            var serviceResponse = new ServiceResponse<ICollection<ResponseTitleDetails>>();
            try
            {
                int subjectID = _context.Subject.FirstOrDefault(s => s.UID == subjectUID.ToLower())!.SubjectID;

                if (subjectID == AppConstants.DEFAULT)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    ICollection<Title> dbTitle = await _context.Title.Where(t => t.SubjectID == subjectID).ToListAsync();

                    if (dbTitle is null || dbTitle.Count == AppConstants.DEFAULT)
                    {
                        serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                        serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                    }
                    else
                    {
                        serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                        serviceResponse.Payload = dbTitle.Select(t => _mapper.Map<ResponseTitleDetails>(t)).ToList();
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

        public async Task<ServiceResponse<ResponseTitleDetails>> GetTitleByID(string uid)
        {
            var serviceResponse = new ServiceResponse<ResponseTitleDetails>();
            try
            {
                var dbTitle = await _context.Title.FirstOrDefaultAsync(t => t.UID == uid.ToLower());

                if (dbTitle is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = _mapper.Map<ResponseTitleDetails>(dbTitle);
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseTitleDetails>> updateTitle(RequestUpdateTitle updateTitle)
        {
            var serviceResponse = new ServiceResponse<ResponseTitleDetails>();
            try
            {

                var targetTitle = await _context.Title.FirstOrDefaultAsync(t => t.UID == updateTitle.UID.ToLower());

                if (targetTitle is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    targetTitle.Name = updateTitle.Name;
                    targetTitle.SubjectID = updateTitle.SubjectID;
                    targetTitle.ModifyDate = DateTime.Now;
                    await _context.SaveChangesAsync();

                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    var updatedTitle = await _context.Title.FirstOrDefaultAsync(s => s.UID == updateTitle.UID.ToLower());
                    serviceResponse.Payload = _mapper.Map<ResponseTitleDetails>(updatedTitle);
                }
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }


        public string GenerateTitleUID(int subjectID)
        {
            int lastID = 1;

            if (_context.Title.Any())
            {
                lastID = _context.Title
                .OrderByDescending(t => t.TitleID)
                .Select(t => t.TitleID)
                .FirstOrDefault() + 1;
            }

            string subUID = _context.Subject.FirstOrDefault(s => s.SubjectID == subjectID)!.UID.ToLower();

            return subUID + "title" + lastID.ToString();
        }
    }
}