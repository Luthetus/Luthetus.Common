using Luthetus.Common.RazorLib.Store.ThemeCase;
using Fluxor;

namespace Luthetus.Common.RazorLib.Theme;

public class ThemeRecordsCollectionService : IThemeRecordsCollectionService
{
    private readonly IDispatcher _dispatcher;

    public ThemeRecordsCollectionService(
        IState<ThemeRecordsCollection> themeRecordsCollectionWrap,
        IDispatcher dispatcher)
    {
        ThemeRecordsCollectionWrap = themeRecordsCollectionWrap;
        _dispatcher = dispatcher;
    }

    public IState<ThemeRecordsCollection> ThemeRecordsCollectionWrap { get; }

    public void RegisterThemeRecord(ThemeRecord themeRecord)
    {
        _dispatcher.Dispatch(new ThemeRecordsCollection.RegisterAction(
            themeRecord));
    }

    public void DisposeThemeRecord(ThemeKey themeKey)
    {
        _dispatcher.Dispatch(new ThemeRecordsCollection.DisposeAction(
            themeKey));
    }
}







