using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportiverseApi.Models.News;

namespace SportiverseApi.Controllers
{
    [ApiController]
    [Route("api/news")]
    public class NewsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public NewsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<News>>> GetNews()
        {
            return Ok(await _dataContext.News.ToListAsync());
        }

        [HttpPost, Authorize(Roles = "Owner")]
        public async Task<ActionResult<List<News>>> AddNews(NewsDto news)
        {
            News _news = new News();
            _news.Title = news.Title;
            _news.Description = news.Description;

            _dataContext.Add(_news);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.News.ToListAsync());
        }

        [HttpPut, Authorize(Roles = "Owner")]
        public async Task<ActionResult<List<News>>> UpdateNews(News updatedNews)
        {
            var news = await _dataContext.News.FindAsync(updatedNews.Id);

            if (news == null) return BadRequest("News item not found");

            news.Title = updatedNews.Title;
            news.Description = updatedNews.Description;
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.News.ToListAsync());
        }

        [HttpDelete("{newsId}"), Authorize(Roles = "Owner")]
        public async Task<ActionResult<List<News>>> DeleteNews(int newsId)
        {
            var news = await _dataContext.News.FindAsync(newsId);

            if (news == null) return BadRequest("News not item found");

            _dataContext.News.Remove(news);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.News.ToListAsync());
        }
    }
}
