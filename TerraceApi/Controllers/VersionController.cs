using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using TerraceApi.Services;

namespace TerraceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VersionController : ControllerBase
{
    public VersionController(VersionService versionService)
    {
        VersionService = versionService;
    }

    public VersionService VersionService { get; set; }

    [HttpGet(Name = "GetVersion")]
    public string Get()
    {
        return VersionService.Get();
    }
}