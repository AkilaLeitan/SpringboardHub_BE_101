using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Service.ArticleService
{
    public interface IArticleService
    {
        Task<ServiceResponse<ICollection<ResponseArticleDetails>>> GetAllArticles();
        Task<ServiceResponse<ICollection<ResponseArticleDetails>>> GetArticleByTitle(string titleUID);
        Task<ServiceResponse<ResponseArticleDetails>> GetArticleByID(string uid);
        Task<ServiceResponse<ResponseArticleDetails>> AddArticle(RequestAddArticle newArticle);
        Task<ServiceResponse<ResponseArticleDetails>> updateArticle(RequestUpdateArticle updateArticle);
    }
}