using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpringboardHub_BE_101.Service.ArticleService;

namespace SpringboardHub_BE_101.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            this._articleService = articleService;
        }

        [HttpGet("GetAllArticles")]
        public async Task<ActionResult<ServiceResponse<ICollection<ResponseArticleDetails>>>> GetAllArticles()
        {
            return Ok(await _articleService.GetAllArticles());
        }

        [HttpGet("GetArticlesByTitle")]
        public async Task<ActionResult<ServiceResponse<ICollection<ResponseArticleDetails>>>> GetArticlesByTitle(string uid)
        {
            return Ok(await _articleService.GetArticleByTitle(uid));
        }

        [HttpGet("GetArticleByID/{uid}")]
        public async Task<ActionResult<ServiceResponse<ResponseArticleDetails>>> GetArticleByID(string uid)
        {
            return Ok(await _articleService.GetArticleByID(uid));
        }

        [HttpPost("AddArticle")]
        public async Task<ActionResult<ServiceResponse<ResponseArticleDetails>>> AddArticle(RequestAddArticle newArticle)
        {
            return Ok(await _articleService.AddArticle(newArticle));
        }

        [HttpPut("UpdateArticle")]
        public async Task<ActionResult<ServiceResponse<ResponseArticleDetails>>> UpdateArticle(RequestUpdateArticle updateArticle)
        {
            return Ok(await _articleService.updateArticle(updateArticle));
        }
    }
}