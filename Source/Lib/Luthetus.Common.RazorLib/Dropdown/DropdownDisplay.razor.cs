using Luthetus.Common.RazorLib.Store.DropdownCase;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Luthetus.Common.RazorLib.Dropdown;

public partial class DropdownDisplay : FluxorComponent
{
    [Inject]
    private IState<DropdownsState> DropdownStatesWrap { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public DropdownKey DropdownKey { get; set; } = null!;
    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = null!;
    [Parameter]
    public DropdownPositionKind DropdownPositionKind { get; set; } = DropdownPositionKind.Vertical;
    [Parameter]
    public bool ShouldDisplayOutOfBoundsClickDisplay { get; set; } = true;
    [Parameter]
    public string CssStyleString { get; set; } = string.Empty;

    private bool ShouldDisplay => DropdownStatesWrap.Value.ActiveDropdownKeys
        .Contains(DropdownKey);

    private string DropdownPositionKindStyleCss => DropdownPositionKind switch
    {
        DropdownPositionKind.Vertical => "position: absolute; left: 0; top: 100%;",
        DropdownPositionKind.Horizontal => "position: absolute; left: 100%; top: 0;",
        DropdownPositionKind.Unset => string.Empty,
        _ => throw new ApplicationException($"The {nameof(DropdownPositionKind)}: {DropdownPositionKind} was unrecognized.")
    };

    private void ClearAllActiveDropdownKeys(MouseEventArgs mouseEventArgs)
    {
        Dispatcher.Dispatch(new DropdownsState.ClearActivesAction());
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (ShouldDisplay)
                Dispatcher.Dispatch(new DropdownsState.RemoveActiveAction(DropdownKey));
        }

        base.Dispose(true);
    }
}


