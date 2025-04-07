using Microsoft.AspNetCore.Mvc;
using WebApiHackerNews.Interfaces;

namespace WebApiHackerNews.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoriesController: ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;

        public StoriesController(IHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }
        [HttpGet("beststories")]
        public async Task<IActionResult> GetBestStories([FromQuery] int count = 10)
        {
            if (count <= 0 || count > 100)
                return BadRequest("Count must be between 1 and 100");

            var stories = await _hackerNewsService.GetTopStoriesAsync(count);
            return Ok(stories);
        }
    }
}
