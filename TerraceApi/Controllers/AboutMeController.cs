using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using TerraceApi.Services;
using TerraceApi.Services.AboutMe;

namespace TerraceApi.Controllers;

public class AboutMeController : ControllerBase
{
    public AboutMeController(GithubService githubService, AmRenderService render)
    {
        GithubService = githubService;
        Render = render;
    }

    public GithubService GithubService { get; set; }
    public AmRenderService Render { get; set; }
    [HttpGet("AboutMeJson")]
    public async Task<IActionResult> AboutMeJson()
    {
        return Ok(await GithubService.GetStatsAsync());
    }
    [HttpGet("AboutMe")]
    public async Task<IActionResult> AboutMe()
    {
        HttpContext.Response.Headers.Add("pragma", "no-cache");
        HttpContext.Response.Headers.Add("expires", "0");
        HttpContext.Response.Headers.Add("cache-control", "no-cache, no-store, must-revalidate, max-age=0");
        var info = new SKImageInfo(AmRenderService.Width, AmRenderService.Height);
        using var surface = SKSurface.Create(info);
        await Render.DrawAboutMeAsync(surface, info);
        // save the file
        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        return new FileStreamResult(data.AsStream(), "image/png");
    }
}