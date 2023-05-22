namespace Luthetus.Common.RazorLib.Theme;

public record ThemeKey(Guid Guid)
{
    public static readonly ThemeKey Empty = new ThemeKey(Guid.Empty);

    public static ThemeKey NewThemeKey()
    {
        return new ThemeKey(Guid.NewGuid());
    }
}