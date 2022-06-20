using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using GitRCFS;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace TerraceApi.Controllers;

[ApiController]
public class RcfsController : ControllerBase
{
    private FileRepository _root;

    public RcfsController(FileRepository root, IHostApplicationLifetime lifetime)
    {
        _root = root;
        lifetime.ApplicationStopping.Register(() =>
        {
            _root.Dispose();
        });
    }
    
    [HttpGet("querypath")]
    public string QueryPath()
    {
        return JsonSerializer.Serialize(_root);
    }

    [HttpGet("querypath/{*path}")]
    public string QueryPath(string path)
    {
        return JsonSerializer.Serialize(_root.ResolvePath(path));
    }
    
    [HttpGet("loadpath/{*path}")]
    public IActionResult LoadPath(string path = "")
    {
        var v = _root.ResolvePath(path);
        const string DefaultContentType = "application/octet-stream";

        var provider = new FileExtensionContentTypeProvider();

        if (!provider.TryGetContentType(v.Name, out string contentType))
        {
            contentType = DefaultContentType;
        }

        return File(v.GetData(), contentType);
    }
    /// <summary>
    /// Renders Markdown into HTML
    /// </summary>
    /// <param name="path"></param>
    /// <returns>Rendered HTML</returns>
    [HttpGet("renderpath/{*path}")]
    public IActionResult RenderPath(string path = "")
    {
        var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        return Ok(Markdown.ToHtml(_root.ResolvePath(path).GetStringData() ?? string.Empty, pipeline));
    }
}