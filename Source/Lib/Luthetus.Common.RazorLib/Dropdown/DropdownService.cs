using Luthetus.Common.RazorLib.Store.DropdownCase;
using Fluxor;

namespace Luthetus.Common.RazorLib.Dropdown;

public class DropdownService : IDropdownService
{
    private readonly IDispatcher _dispatcher;

    public DropdownService(
        bool isEnabled,
        IDispatcher dispatcher,
        IState<DropdownsState> dialogStateWrap)
    {
        _dispatcher = dispatcher;
        IsEnabled = isEnabled;
        DropdownsStateWrap = dialogStateWrap;
    }

    public bool IsEnabled { get; }
    public IState<DropdownsState> DropdownsStateWrap { get; }

    public void AddActiveDropdownKey(DropdownKey dialogRecord)
    {
        _dispatcher.Dispatch(new DropdownsState.AddActiveAction(
            dialogRecord));
    }

    public void RemoveActiveDropdownKey(DropdownKey dropdownKey)
    {
        _dispatcher.Dispatch(new DropdownsState.RemoveActiveAction(
            dropdownKey));
    }

    public void ClearActiveDropdownKeysAction()
    {
        _dispatcher.Dispatch(new DropdownsState.ClearActivesAction());
    }
}







