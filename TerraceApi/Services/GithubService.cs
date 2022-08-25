using Microsoft.Extensions.Caching.Memory;
using Octokit;
using TerraceApi.Models;

namespace TerraceApi.Services;

public class GithubService
{
    public GithubService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public string Username { get; } = "encodeous";
    public GitHubClient Client = new (new ProductHeaderValue("encodeous-terrace-api"));
    public IMemoryCache _cache;
    private const string GH_REPO_CACHE = "T_GITHUB";

    public async Task<GithubStats> GetStatsAsync()
    {
        if (!_cache.TryGetValue(GH_REPO_CACHE, out GithubStats stats))
        {
            var user = await Client.User.Get(Username);
            stats = new GithubStats()
            {
                LastUpdated = DateTime.UtcNow,
                Followers = user.Followers,
                Following = user.Following,
                Repos = (await Client.Repository.GetAllForUser(Username)).ToArray()
            };
            stats.RecentlyUpdated = stats.Repos.OrderByDescending(x => x.PushedAt).ToArray();
            stats.Forks = stats.Repos.Count(x => x.Fork);
            stats.Stars = stats.Repos.Sum(x =>
            {
                if (!x.Fork)
                {
                    return x.StargazersCount;
                }
                return 0;
            });
            _cache.Set(GH_REPO_CACHE, stats, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
        }
        return stats;
    }
}