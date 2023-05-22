using Luthetus.Common.RazorLib.Store.ApplicationOptions;
using Luthetus.Common.RazorLib.Store.ThemeCase;
using Fluxor;
using Luthetus.Common.RazorLib.Dimensions;
using Luthetus.Common.RazorLib.Storage;
using Luthetus.Common.RazorLib.Store.StorageCase;
using Luthetus.Common.RazorLib.Theme;

namespace Luthetus.Common.RazorLib.Options;

public class AppOptionsService : IAppOptionsService
{
    private readonly IDispatcher _dispatcher;
    private readonly IStorageService _storageService;

    public AppOptionsService(
        bool isEnabled,
        IState<AppOptionsState> applicationOptionsState,
        IState<ThemeRecordsCollection> themeRecordsCollectionWrap,
        IDispatcher dispatcher,
        IStorageService storageService)
    {
        IsEnabled = isEnabled;
        AppOptionsStateWrap = applicationOptionsState;
        ThemeRecordsCollectionWrap = themeRecordsCollectionWrap;
        _dispatcher = dispatcher;
        _storageService = storageService;
    }

    public bool IsEnabled { get; }
    public IState<AppOptionsState> AppOptionsStateWrap { get; }
    public IState<ThemeRecordsCollection> ThemeRecordsCollectionWrap { get; }

    public string StorageKey => "luthetus-common_theme-storage-key";

    public string ThemeCssClassString => ThemeRecordsCollectionWrap.Value.ThemeRecordsList
                                                   .FirstOrDefault(x =>
                                                       x.ThemeKey == AppOptionsStateWrap.Value.Options
                                                           .ThemeKey)
                                                   ?.CssClassString
                                               ?? ThemeFacts.VisualStudioDarkThemeClone.CssClassString;

    public string? FontFamilyCssStyleString
    {
        get
        {
            if (AppOptionsStateWrap.Value.Options.FontFamily is null)
                return null;

            return $"font-family: {AppOptionsStateWrap.Value.Options.FontFamily};";
        }
    }


    public string FontSizeCssStyleString
    {
        get
        {
            var fontSizeInPixels = AppOptionsStateWrap.Value.Options.FontSizeInPixels ??
                                   AppOptionsState.DEFAULT_FONT_SIZE_IN_PIXELS;

            var fontSizeInPixelsCssValue = fontSizeInPixels.ToCssValue();

            return $"font-size: {fontSizeInPixelsCssValue}px;";
        }
    }

    public void SetActiveThemeRecordKey(
        ThemeKey themeKey,
        bool updateStorage = true)
    {
        _dispatcher.Dispatch(
            new AppOptionsState.SetAppOptionsStateAction(
                inApplicationOptionsState => new AppOptionsState
                {
                    Options = inApplicationOptionsState.Options with
                    {
                        ThemeKey = themeKey
                    }
                }));

        if (updateStorage)
            WriteToStorage();
    }

    public void SetTheme(
        ThemeRecord theme,
        bool updateStorage = true)
    {
        _dispatcher.Dispatch(
            new AppOptionsState.SetAppOptionsStateAction(
                inAppOptionsState => new AppOptionsState
                {
                    Options = inAppOptionsState.Options with
                    {
                        ThemeKey = theme.ThemeKey
                    }
                }));

        if (updateStorage)
            WriteToStorage();
    }

    public void SetFontFamily(
        string? fontFamily,
        bool updateStorage = true)
    {
        _dispatcher.Dispatch(
            new AppOptionsState.SetAppOptionsStateAction(
                inAppOptionsState => new AppOptionsState
                {
                    Options = inAppOptionsState.Options with
                    {
                        FontFamily = fontFamily
                    }
                }));

        if (updateStorage)
            WriteToStorage();
    }

    public void SetFontSize(
        int fontSizeInPixels,
        bool updateStorage = true)
    {
        _dispatcher.Dispatch(
            new AppOptionsState.SetAppOptionsStateAction(
                inAppOptionsState => new AppOptionsState
                {
                    Options = inAppOptionsState.Options with
                    {
                        FontSizeInPixels = fontSizeInPixels
                    }
                }));

        if (updateStorage)
            WriteToStorage();
    }

    public void SetIconSize(
        int iconSizeInPixels,
        bool updateStorage = true)
    {
        _dispatcher.Dispatch(
            new AppOptionsState.SetAppOptionsStateAction(
                inAppOptionsState => new AppOptionsState
                {
                    Options = inAppOptionsState.Options with
                    {
                        IconSizeInPixels = iconSizeInPixels
                    }
                }));

        if (updateStorage)
            WriteToStorage();
    }

    public async Task SetFromLocalStorageAsync()
    {
        var optionsJsonString = await _storageService
                .GetValue(StorageKey)
            as string;

        if (string.IsNullOrWhiteSpace(optionsJsonString))
            return;

        var options = System.Text.Json.JsonSerializer
            .Deserialize<CommonOptions>(optionsJsonString);

        if (options is null)
            return;

        if (options.ThemeKey is not null)
        {
            var matchedTheme = ThemeRecordsCollectionWrap.Value.ThemeRecordsList
                .FirstOrDefault(x =>
                    x.ThemeKey == options.ThemeKey);

            SetTheme(matchedTheme ?? ThemeFacts.VisualStudioDarkThemeClone,
                false);
        }

        if (options.FontFamily is not null)
        {
            SetFontFamily(options.FontFamily,
                false);
        }

        if (options.FontSizeInPixels is not null)
        {
            SetFontSize(options.FontSizeInPixels.Value,
                false);
        }

        if (options.IconSizeInPixels is not null)
        {
            SetIconSize(options.IconSizeInPixels.Value,
                false);
        }
    }

    public void WriteToStorage()
    {
        _dispatcher.Dispatch(
            new StorageEffects.WriteToStorageAction(
                StorageKey,
                AppOptionsStateWrap.Value.Options));
    }
}







