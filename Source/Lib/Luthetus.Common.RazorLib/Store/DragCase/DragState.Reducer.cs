using Fluxor;

namespace Luthetus.Common.RazorLib.Store.DragCase;

public partial class DragState
{
    private class Reducer
    {
        [ReducerMethod]
        public static DragState ReduceSetDragStateAction(
            DragState inDragState,
            SetDragStateAction setDragStateAction)
        {
            return new DragState
            {
                ShouldDisplay = setDragStateAction.ShouldDisplay,
                MouseEventArgs = setDragStateAction.MouseEventArgs
            };
        }
    }
}