namespace Luthetus.Common.RazorLib.Dimensions;

public static class CssInterpolation
{
    public static string ToCssValue(this double value) =>
        value.ToString(System.Globalization.CultureInfo.InvariantCulture);

    public static string ToCssValue(this decimal value) =>
        value.ToString(System.Globalization.CultureInfo.InvariantCulture);

    public static string ToCssValue(this float value) =>
        value.ToString(System.Globalization.CultureInfo.InvariantCulture);

    public static string ToCssValue(this int value) =>
        value.ToString(System.Globalization.CultureInfo.InvariantCulture);
}