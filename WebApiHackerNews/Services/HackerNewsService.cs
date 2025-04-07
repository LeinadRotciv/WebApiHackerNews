using Microsoft.Extensions.Caching.Memory;
using WebApiHackerNews.DTO;
using WebApiHackerNews.Interfaces;

namespace WebApiHackerNews.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const string Url = "https://hacker-news.firebaseio.com/v0/";

        public HackerNewsService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }
        public async Task<List<StoryDto>> GetTopStoriesAsync(int count)
        {
            var storyIds = await GetBestStoryIdsAsync();
            var topStories = new List<StoryDto>();

            var tasks = storyIds.Take(100).Select(id => GetStoryByIdAsync(id));

            var stories = await Task.WhenAll(tasks);
            topStories = stories.Where(s => s != null)
                .OrderByDescending(s => s.Score).Take(count).ToList();

            return topStories;
        }
        private async Task<List<int>> GetBestStoryIdsAsync()
        {
            return await _cache.GetOrCreateAsync("bestStoryIds", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return await _httpClient.GetFromJsonAsync<List<int>>(Url + "beststories.json");
            });
        }
        private async Task<StoryDto> GetStoryByIdAsync(int id)
        {
            string cacheId = $"story_{id}";

            return await _cache.GetOrCreateAsync(cacheId, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);

                var item = await _httpClient.GetFromJsonAsync<HackerNewsItem>($"{Url}item/{id}.json");

                if (item == null || item.Type != "story")
                    return null;

                return new StoryDto
                {
                    Title = item.Title,
                    Uri = item.Url,
                    PostedBy = item.By,
                    Time = DateTimeOffset.FromUnixTimeSeconds(item.Time).UtcDateTime,
                    Score = item.Score,
                    CommentCount = item.Descendants
                };
            });
        }
        private class HackerNewsItem
        {
            public string By { get; set; }
            public int Descendants { get; set; }
            public int Id { get; set; }
            public List<int> Kids { get; set; }
            public int Score { get; set; }
            public int Time { get; set; }
            public string Title { get; set; }
            public string Type { get; set; }
            public string Url { get; set; }
        }
    }
}
