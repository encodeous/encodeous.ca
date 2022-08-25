using System.Drawing;
using Humanizer;
using SkiaSharp;
using TerraceApi.Utils;

namespace TerraceApi.Services.AboutMe;

public class AmRenderService
{
    public AmRenderService(SkiaService skia, GithubService github)
    {
        Skia = skia;
        Github = github;
        plain = new SKPaint
        {
            Color = SKColors.WhiteSmoke,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextAlign = SKTextAlign.Left,
            TextSize = 80,
            Typeface = Skia.Raleway(1),
        };
        light = new SKPaint
        {
            Color = SKColors.WhiteSmoke,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextAlign = SKTextAlign.Left,
            TextSize = 49,
            Typeface = Skia.Raleway(-1),
        };
        stylized = new SKPaint
        {
            Color = SKColors.White,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextAlign = SKTextAlign.Left,
            TextSize = 80,
            Typeface = Skia.Pacifico(),
            Shader = GradientA
        };
        consoleStyle = new SKPaint
        {
            Color = SKColors.White,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextAlign = SKTextAlign.Left,
            TextSize = 40,
            Typeface = Skia.FiraCode()
        };
    }

    public SkiaService Skia { get; set; }
    public GithubService Github { get; set; }
    public const int Width = 1724;
    public const int Height = 1548;
    private SKRect Area = new SKRect(0, 0, Width, Height);
    private SKPaint plain;
    private SKPaint light;
    private SKPaint stylized;
    private SKPaint consoleStyle;
    private readonly SKColor mutedColor = SKColor.Parse("#27333B");
    private readonly SKColor hightlightColor = SKColor.Parse("#565B0A");

    private readonly string[] messages = {
        "also known as, Adam",
        "you can also call me Adam",
        "cache is pronounced \"kash\"",
        "greetings from Canada",
        "boo! scared ya",
        "i know you're going to refresh this.",
        "hello, world!",
    };
    
    private SKShader GradientA = SKShader.CreateLinearGradient(
        new SKPoint(0, 0),
        new SKPoint(Width, Height),
        new [] { SKColors.Aqua, SKColors.Navy },
        new float[] { 0, 1 },
        SKShaderTileMode.Repeat);
    private SKShader GradientBG = SKShader.CreateLinearGradient(
        new SKPoint(Width/2, -Height / 2),
        new SKPoint(Width, Height),
        new [] { SKColor.Parse("#2B2A28"), SKColor.Parse("#101D28") },
        new float[] { 0, 1 },
        SKShaderTileMode.Repeat);

    public async Task DrawReadmeAsync(SKSurface surface, SKImageInfo info)
    {
        // the the canvas and properties
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.Transparent);
        // make sure the canvas is blank
        canvas.DrawRoundRect(Area, new SKSize(25, 25), new SKPaint()
        {
            Shader = GradientBG,
            IsAntialias = true,
            IsDither = true
        });
        DrawGreeting(canvas, info);
        DrawAboutMe(canvas, 150, 250);
        await DrawStatsAsync(canvas, 150, 950);
        await DrawRecentReposAsync(canvas, 750, 950);
        var smallStyle = consoleStyle.WithTextSize(30);
        canvas.DrawText($"{DateTime.UtcNow.ToString("O")}                                              .NET {Environment.Version} & Skia", new SKPoint(25, Height - 47 + smallStyle.TextSize / 2), smallStyle);
    }

    private void DrawTitle(SKCanvas canvas, float x, float y, string text, SKColor? color = null)
    {
        var paint = plain.WithTextSize(70).WithTypeFace(Skia.Raleway(1));
        var w = paint.MeasureText(text);
        var offset = paint.TextSize / 2 - 2;
        if (color == null) color = hightlightColor;
        canvas.DrawRect(new SKRect(x, y - 10, x + w, y + offset + 20), new SKPaint()
        {
            Shader = Skia.Stripe(20, color.Value)
        });
        canvas.DrawText(text, new SKPoint(x, y + paint.TextSize / 2), paint);
    }
    private async Task DrawStatsAsync(SKCanvas canvas, float x, float y)
    {
        var stats = await Github.GetStatsAsync();
        DrawTitle(canvas, x, y, "my github stats");
        canvas.DrawText($"fetched {stats.LastUpdated.Humanize()}", 
            new SKPoint(x + 520, y + 70), consoleStyle.WithTextSize(20).WithAlignment(SKTextAlign.Right));
        float offset = 140;
        DrawLine($"followers     -> {stats.Followers}", x + 10, y, canvas, consoleStyle, ref offset);
        DrawLine($"following     -> {stats.Following}", x + 10, y, canvas, consoleStyle, ref offset);
        DrawLine($"forks created -> {stats.Forks}", x + 10, y, canvas, consoleStyle, ref offset);
        DrawLine($"stars earned  -> {stats.Stars}", x + 10, y, canvas, consoleStyle, ref offset);
        DrawLine($"public repos  -> {stats.Repos.Length}", x + 10, y, canvas, consoleStyle, ref offset);
    }

    private async Task DrawRecentReposAsync(SKCanvas canvas, float x, float y)
    {
        var stats = await Github.GetStatsAsync();
        DrawTitle(canvas, x, y, "recently updated repos");
        float offset = 140;
        foreach (var repo in stats.RecentlyUpdated.Take(6))
        {
            DrawLine($"{repo.FullName} - {repo.PushedAt.Humanize()}", x + 10, y, canvas, consoleStyle.WithTextSize(30), ref offset);
        }
    }

    private void DrawAboutMe(SKCanvas canvas, float x, float y)
    {
        DrawTitle(canvas, x, y, "a little about myself", SKColor.Parse("#376939"));
    }
    private void DrawGreeting(SKCanvas canvas, SKImageInfo info)
    {
        canvas.DrawText("Hey there! I'm ", new SKPoint(100, 100 + plain.TextSize / 2), plain);
        canvas.DrawText("encodeous", new SKPoint(650, 100 + stylized.TextSize / 2), stylized);
        canvas.RotateDegrees(-3);
        canvas.DrawText(messages[Random.Shared.Next(messages.Length)], new SKPoint(1170, 210 + light.TextSize / 2), light.WithAlignment(SKTextAlign.Right).WithTextSize(40));
        canvas.RotateDegrees(3);
        DrawEmoji(canvas, 980, 100 + plain.TextSize / 2, "👋", plain);
        // canvas.DrawText("This page is served fresh to you from Canada", new SKPoint(330, 400 + consoleStyle.TextSize / 2), consoleStyle);
    }

    private void DrawEmoji(SKCanvas canvas, float x, float y, string character, SKPaint paint)
    {
        canvas.DrawText(character, x, y, paint.WithTypeFace(Skia.Twemoji()));
    }

    private void DrawLine(string text, float x, float y, SKCanvas canvas, SKPaint paint, ref float offset)
    {
        if (!string.IsNullOrEmpty(text))
        {
            canvas.DrawText(text, x, y + offset, paint);
        }
        offset += paint.FontSpacing + paint.TextSize / 2;
    }
}