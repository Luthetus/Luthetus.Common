using System.Collections.Immutable;
using Fluxor;
using Luthetus.Common.RazorLib.Theme;

namespace Luthetus.Common.RazorLib.Store.ThemeCase;

/// <summary>
/// Keep the <see cref="ThemeRecordsCollection"/> as a class
/// as to avoid record value comparisons when Fluxor checks
/// if the <see cref="FeatureStateAttribute"/> has been replaced.
/// </summary>
[FeatureState]
public partial class ThemeRecordsCollection
{
    public ThemeRecordsCollection()
    {
        ThemeRecordsList = new []
        {
            ThemeFacts.VisualStudioDarkThemeClone,
            ThemeFacts.VisualStudioLightThemeClone,
        }.ToImmutableList();
    }

    public ImmutableList<ThemeRecord> ThemeRecordsList { get; init; } 
}