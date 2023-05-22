using Luthetus.Common.RazorLib.Store.ThemeCase;
using Fluxor;

namespace Luthetus.Common.RazorLib.Theme;

public class ThemeService : IThemeService
{
    private readonly IDispatcher _dispatcher;

    public ThemeService(
        bool isEnabled,
        IState<ThemeRecordsCollection> themeRecordsCollectionWrap,
        IDispatcher dispatcher)
    {
        IsEnabled = isEnabled;
        ThemeRecordsCollectionWrap = themeRecordsCollectionWrap;
        _dispatcher = dispatcher;
    }

    public bool IsEnabled { get; }
    public IState<ThemeRecordsCollection> ThemeRecordsCollectionWrap { get; }
}