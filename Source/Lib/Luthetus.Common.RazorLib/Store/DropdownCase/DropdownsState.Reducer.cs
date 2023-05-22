using Fluxor;

namespace Luthetus.Common.RazorLib.Store.DropdownCase;

public partial record DropdownsState
{
    private class Reducer
    {
        [ReducerMethod]
        public static DropdownsState ReduceAddActiveAction(
            DropdownsState inDropdownsState,
            AddActiveAction addActiveAction)
        {
            return inDropdownsState with
            {
                ActiveDropdownKeys = inDropdownsState.ActiveDropdownKeys
                    .Add(addActiveAction.DropdownKey)
            };
        }
    
        [ReducerMethod]
        public static DropdownsState ReduceRemoveActiveAction(
            DropdownsState inDropdownsState,
            RemoveActiveAction removeActiveAction)
        {
            return inDropdownsState with
            {
                ActiveDropdownKeys = inDropdownsState.ActiveDropdownKeys
                    .Remove(removeActiveAction.DropdownKey)
            };
        }
    
        [ReducerMethod(typeof(ClearActivesAction))]
        public static DropdownsState ReduceClearActivesAction(
            DropdownsState inDropdownsState)
        {
            return inDropdownsState with
            {
                ActiveDropdownKeys = inDropdownsState.ActiveDropdownKeys
                    .Clear()
            };
        }
    }
}