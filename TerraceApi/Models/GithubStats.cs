using Octokit;

namespace TerraceApi.Models;

public class GithubStats
{
    public Repository[] Repos { get; set; }
    public int Followers { get; set; }
    public int Following { get; set; }
    public int Forks { get; set; }
    public int Stars { get; set; }
    public Repository[] RecentlyUpdated { get; set; }
    public DateTime LastUpdated { get; set; }
}