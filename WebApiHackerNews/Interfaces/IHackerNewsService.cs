using WebApiHackerNews.DTO;

namespace WebApiHackerNews.Interfaces
{
    public interface IHackerNewsService
    {
        Task<List<StoryDto>> GetTopStoriesAsync(int count);
    }
}
