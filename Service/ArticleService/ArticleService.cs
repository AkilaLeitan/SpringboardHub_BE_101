using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.ArticleService
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ArticleService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<ResponseArticleDetails>> AddArticle(RequestAddArticle newArticle)
        {
            var serviceResponse = new ServiceResponse<ResponseArticleDetails>();
            try
            {
                if (!_context.Title.Any(t => t.TitleID == newArticle.TitleID))
                {
                    throw new Exception("Title not found!");
                }
                else if (!_context.Lecture.Any(l => l.LectureID == newArticle.LectureID))
                {
                    throw new Exception("Lecture not found!");
                }
                else
                {
                    var article = _mapper.Map<Article>(newArticle);
                    article.UID = GenerateArticleUID(article.TitleID);
                    article.CreatedDate = DateTime.Now;
                    article.isUpdated = false;

                    _context.Article.Add(article);
                    await _context.SaveChangesAsync();

                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = _mapper.Map<ResponseArticleDetails>(_context.Article.OrderByDescending(a => a.ArticleID).FirstOrDefault());
                }
            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ICollection<ResponseArticleDetails>>> GetAllArticles()
        {
            var serviceResponse = new ServiceResponse<ICollection<ResponseArticleDetails>>();
            try
            {
                ICollection<Article> dbArticle = await _context.Article.ToListAsync();

                if (dbArticle is null || dbArticle.Count == AppConstants.DEFAULT)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = dbArticle.Select(a => _mapper.Map<ResponseArticleDetails>(a)).ToList();
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseArticleDetails>> GetArticleByID(string uid)
        {
            var serviceResponse = new ServiceResponse<ResponseArticleDetails>();
            try
            {
                var dbArticle = await _context.Article.FirstOrDefaultAsync(a => a.UID == uid.ToLower());

                if (dbArticle is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                    serviceResponse.Payload = _mapper.Map<ResponseArticleDetails>(dbArticle);
                }

            }
            catch (Exception e)
            {
                serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR;
                serviceResponse.ResponseMessage = e.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ICollection<ResponseArticleDetails>>> GetArticleByTitle(string titleUID)
        {
            var serviceResponse = new ServiceResponse<ICollection<ResponseArticleDetails>>();
            try
            {
                int titleID = _context.Title.FirstOrDefault(t => t.UID == titleUID.ToLower())!.SubjectID;

                if (titleID == AppConstants.DEFAULT)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    ICollection<Article> dbArticle = await _context.Article.Where(a => a.TitleID == titleID).ToListAsync();

                    if (dbArticle is null || dbArticle.Count == AppConstants.DEFAULT)
                    {
                        serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                        serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                    }
                    else
                    {
                        serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                        serviceResponse.Payload = dbArticle.Select(a => _mapper.Map<ResponseArticleDetails>(a)).ToList();
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

        public async Task<ServiceResponse<ResponseArticleDetails>> updateArticle(RequestUpdateArticle updateArticle)
        {
            var serviceResponse = new ServiceResponse<ResponseArticleDetails>();
            try
            {

                var targetArticle = await _context.Article.FirstOrDefaultAsync(a => a.UID == updateArticle.UID.ToLower());

                if (targetArticle is null)
                {
                    serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_NOTFOUND;
                    serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_NOTFOUND;
                }
                else
                {
                    if (!_context.Title.Any(t => t.TitleID == updateArticle.TitleID))
                    {
                        throw new Exception("Title not found!");
                    }
                    else if (!_context.Lecture.Any(l => l.LectureID == updateArticle.LectureID))
                    {
                        throw new Exception("Lecture not found!");
                    }
                    else
                    {
                        targetArticle.Topic = updateArticle.Topic;
                        targetArticle.Description = updateArticle.Description;
                        targetArticle.visibaleType = updateArticle.visibaleType;
                        targetArticle.LectureID = updateArticle.LectureID;
                        targetArticle.TitleID = updateArticle.TitleID;
                        targetArticle.ModifyDate = DateTime.Now;
                        targetArticle.isUpdated = true;

                        await _context.SaveChangesAsync();

                        serviceResponse.ResponseCode = AppConstants.DEFAULT_RESPONSE_CODE_SUCCSSES;
                        serviceResponse.ResponseMessage = AppConstants.DEFAULT_RESPONSE_MESSAGE_SUCCSSES;
                        var updatedArticle = await _context.Article.FirstOrDefaultAsync(a => a.UID == updateArticle.UID.ToLower());
                        serviceResponse.Payload = _mapper.Map<ResponseArticleDetails>(updatedArticle);
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

        private string GenerateArticleUID(int titleID)
        {
            int lastID = 1;

            if (_context.Article.Any())
            {
                lastID = _context.Article
                .OrderByDescending(a => a.ArticleID)
                .Select(a => a.ArticleID)
                .FirstOrDefault() + 1;
            }

            string tID = _context.Title.FirstOrDefault(t => t.TitleID == titleID)!.UID.ToLower();

            return tID + "article" + lastID.ToString();
        }
    }
}