using Fluxor;

namespace Luthetus.Common.RazorLib.Store.RenderTrackerCase;

public partial class RenderTrackerState
{
    private class Reducer
    {
        [ReducerMethod]
        public static RenderTrackerState ReduceRegisterAction(
            RenderTrackerState inRenderTrackerState,
            RegisterAction registerAction)
        {
            if (inRenderTrackerState.Map.ContainsKey(
                registerAction.RenderTrackerObject.DisplayName))
            {
                return inRenderTrackerState;
            }

            var outRenderTrackerState = new RenderTrackerState(
                inRenderTrackerState.Map.Add(
                    registerAction.RenderTrackerObject.DisplayName,
                    registerAction.RenderTrackerObject));

            return outRenderTrackerState;
        }
        
        [ReducerMethod]
        public static RenderTrackerState ReduceDisposeAction(
            RenderTrackerState inRenderTrackerState,
            DisposeAction disposeAction)
        {
            if (!inRenderTrackerState.Map.ContainsKey(
                disposeAction.RenderTrackerObjectDisplayName))
            {
                return inRenderTrackerState;
            }

            var outRenderTrackerState = new RenderTrackerState(
                inRenderTrackerState.Map.Remove(
                    disposeAction.RenderTrackerObjectDisplayName));

            return outRenderTrackerState;
        }
        
        [ReducerMethod]
        public static RenderTrackerState ReduceAddEntryAction(
            RenderTrackerState inRenderTrackerState,
            AddEntryAction addEntryAction)
        {
            if (!inRenderTrackerState.Map.TryGetValue(
                addEntryAction.RenderTrackerObjectDisplayName,
                out var inRenderTrackerObject))
            {
                return inRenderTrackerState;
            }

            var outRenderTrackerEntries = inRenderTrackerObject.RenderTrackerEntries
                .Add(addEntryAction.RenderTrackerEntry);

            var outRenderTrackerState = new RenderTrackerState(
                inRenderTrackerState.Map.SetItem(
                    inRenderTrackerObject.DisplayName,
                    new(
                        inRenderTrackerObject,
                        outRenderTrackerEntries)));

            return outRenderTrackerState;
        }
    }
}
