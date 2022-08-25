using SkiaSharp;

namespace TerraceApi.Utils;

public static class SkiaExtensions
{
    public static SKPaint WithTextSize(this SKPaint paint, float newSize)
    {
        var clone = paint.Clone();
        clone.TextSize = newSize;
        return clone;
    }
    public static SKPaint WithAlignment(this SKPaint paint, SKTextAlign newAlign)
    {
        var clone = paint.Clone();
        clone.TextAlign = newAlign;
        return clone;
    }
    public static SKPaint WithTypeFace(this SKPaint paint, SKTypeface newFace)
    {
        var clone = paint.Clone();
        clone.Typeface = newFace;
        return clone;
    }
}