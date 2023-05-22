using Luthetus.Common.RazorLib.Store.DragCase;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Luthetus.Common.RazorLib.Drag;

public partial class DragInitializer : FluxorComponent
{
    [Inject]
    private IState<DragState> DragStateWrap { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    private string StyleCss => DragStateWrap.Value.ShouldDisplay
        ? string.Empty
        : "display: none;";

    private DragState.SetDragStateAction ConstructClearDragStateAction() =>
        new DragState.SetDragStateAction(false, null);

    private void DispatchSetDragStateActionOnMouseMove(MouseEventArgs mouseEventArgs)
    {
        if ((mouseEventArgs.Buttons & 1) != 1)
        {
            Dispatcher.Dispatch(ConstructClearDragStateAction());
        }
        else
        {
            Dispatcher.Dispatch(new DragState.SetDragStateAction(true, mouseEventArgs));
        }
    }

    private void DispatchSetDragStateActionOnMouseUp()
    {
        Dispatcher.Dispatch(ConstructClearDragStateAction());
    }
}