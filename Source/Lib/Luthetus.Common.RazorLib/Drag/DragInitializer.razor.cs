using Luthetus.Common.RazorLib.Store.DragCase;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Luthetus.Common.RazorLib.Reactive;

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

    private IThrottle _throttleDispatchSetDragStateActionOnMouseMove = new Throttle(IThrottle.DefaultThrottleTimeSpan);

    private DragState.SetDragStateAction ConstructClearDragStateAction() =>
        new DragState.SetDragStateAction(false, null);

    private async Task DispatchSetDragStateActionOnMouseMoveAsync(MouseEventArgs mouseEventArgs)
    {
        await _throttleDispatchSetDragStateActionOnMouseMove.FireAsync(async () =>
        {
            if ((mouseEventArgs.Buttons & 1) != 1)
            {
                Dispatcher.Dispatch(ConstructClearDragStateAction());
            }
            else
            {
                Dispatcher.Dispatch(new DragState.SetDragStateAction(true, mouseEventArgs));
            }
        });
    }

    private async Task DispatchSetDragStateActionOnMouseUpAsync()
    {
        await _throttleDispatchSetDragStateActionOnMouseMove.FireAsync(async () =>
        {
            Dispatcher.Dispatch(ConstructClearDragStateAction());
        });
    }
}