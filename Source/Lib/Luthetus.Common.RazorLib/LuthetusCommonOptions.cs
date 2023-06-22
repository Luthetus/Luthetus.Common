using Luthetus.Common.RazorLib.Theme;

namespace Luthetus.Common.RazorLib;

public record LuthetusCommonOptions
{
    /// <summary>The <see cref="ThemeKey"/> to be used when the application starts</summary>
    public ThemeKey InitialThemeKey { get; init; } = ThemeFacts.VisualStudioDarkThemeClone.ThemeKey;
    public LuthetusCommonFactories LuthetusCommonFactories { get; init; } = new();
}