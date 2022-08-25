using System.Reflection;
using SkiaSharp;

namespace TerraceApi.Services;

public class SkiaService
{
    private Dictionary<string, SKTypeface> _cachedFonts = new();

    public SKTypeface Raleway(int weight = 0)
    {
        return weight switch
        {
            -1 => GetTypeface("Raleway-Light.ttf"),
            0 => GetTypeface("Raleway-Regular.ttf"),
            1 => GetTypeface("Raleway-Bold.ttf"),
            _ => throw new ArgumentOutOfRangeException(nameof(weight), weight, null)
        };
    }

    public SKTypeface Pacifico()
    {
        return GetTypeface("Pacifico-Regular.ttf");
    }
    public SKTypeface FiraCode()
    {
        return GetTypeface("FiraCode-Retina.ttf");
    }
    public SKTypeface GetTypeface(string fullFontName)
    {
        if (_cachedFonts.ContainsKey(fullFontName)) return _cachedFonts[fullFontName];
        SKTypeface result;

        var assembly = Assembly.GetExecutingAssembly();
        var stream = assembly.GetManifestResourceStream("TerraceApi.Font." + fullFontName);
        if (stream == null)
            return null;

        result = SKTypeface.FromStream(stream);
        _cachedFonts[fullFontName] = result;
        return result;
    }

    public SKShader Stripe(float size, SKColor color)
    {
        return SKShader.CreateLinearGradient(
            new SKPoint(size, 0),
            new SKPoint(0, size),
            new [] { color, color, SKColors.Transparent, SKColors.Transparent,  },
            new float[] { 0, 0.5f, 0.5000001f, 1 },
            SKShaderTileMode.Repeat);
    }
}