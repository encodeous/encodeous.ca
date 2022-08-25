using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace TerraceApi.Services;

public class VersionService
{
    private static string _version =
        FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion[..7];

    [HttpGet(Name = "GetVersion")]
    public string Get()
    {
        return _version;
    }
}