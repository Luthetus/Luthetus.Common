using Fluxor;

namespace Luthetus.Common.RazorLib.Store.ApplicationOptions;

public partial class AppOptionsState
{
    private class Reducer
    {
        [ReducerMethod]
        public static AppOptionsState ReduceSetAppOptionsStateWithAction(
            AppOptionsState inAppOptionsState,
            SetAppOptionsStateAction setAppOptionsStateAction)
        {
            var nextSharedOptionsGlobal = setAppOptionsStateAction.WithFunc
                .Invoke(inAppOptionsState);

            return nextSharedOptionsGlobal;
        }
    }
}