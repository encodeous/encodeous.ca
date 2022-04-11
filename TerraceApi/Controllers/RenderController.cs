using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using GitRCFS;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace TerraceApi.Controllers;

[ApiController]
public class RenderController : ControllerBase
{
    /// <summary>
    /// Renders Markdown into HTML
    /// </summary>
    /// <param name="markdownContent"></param>
    /// <returns>Rendered HTML</returns>
    [HttpPost("render")]
    public IActionResult RenderMarkdown([FromBody] JsonElement markdownContent)
    {
        return Ok(Markdown.ToHtml(markdownContent.GetString() ?? string.Empty));
    }
}