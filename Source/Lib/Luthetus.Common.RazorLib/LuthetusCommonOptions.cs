using Luthetus.Common.RazorLib.Theme;

namespace Luthetus.Common.RazorLib;

public record LuthetusCommonOptions
{
    /// <summary>
    /// If one already is using Fluxor they can set this property
    /// to false and then in
    ///
    /// AddFluxor(options => options.ScanAssemblies(...))
    ///
    /// Include typeof(LuthetusCommonOptions).Assembly
    /// when invoking ScanAssemblies for AddFluxor
    /// service collection extension method
    /// </summary>
    public bool InitializeFluxor { get; init; } = true;
    /// <summary>
    /// The <see cref="ThemeKey"/> to be used when the application starts
    /// </summary>
    public ThemeKey InitialThemeKey { get; } = ThemeFacts.VisualStudioDarkThemeClone.ThemeKey;

    public LuthetusCommonFactories LuthetusCommonFactories { get; init; } = new();
}