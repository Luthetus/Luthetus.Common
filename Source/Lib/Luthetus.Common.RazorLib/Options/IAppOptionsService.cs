using Luthetus.Common.RazorLib.Store.ApplicationOptions;
using Fluxor;
using Luthetus.Common.RazorLib.Storage;
using Luthetus.Common.RazorLib.Theme;

namespace Luthetus.Common.RazorLib.Options;

public interface IAppOptionsService : ILuthetusCommonService
{
    /// <summary>This is used when interacting with the <see cref="IStorageService"/> to set and get data.</summary>
    public string StorageKey { get; }
    public IState<AppOptionsState> AppOptionsStateWrap { get; }
    public string ThemeCssClassString { get; }
    public string? FontFamilyCssStyleString { get; }
    public string FontSizeCssStyleString { get; }

    public void SetActiveThemeRecordKey(ThemeKey themeKey, bool updateStorage = true);
    public void SetTheme(ThemeRecord theme, bool updateStorage = true);
    public void SetFontFamily(string? fontFamily, bool updateStorage = true);
    public void SetFontSize(int fontSizeInPixels, bool updateStorage = true);
    public void SetIconSize(int iconSizeInPixels, bool updateStorage = true);
    public Task SetFromLocalStorageAsync();
    public void WriteToStorage();
}