using Microsoft.AspNetCore.Components.Web;

namespace Luthetus.Common.RazorLib.Store.DragCase;

public partial class DragState
{
    public record SetDragStateAction(bool ShouldDisplay, MouseEventArgs? MouseEventArgs);
}
