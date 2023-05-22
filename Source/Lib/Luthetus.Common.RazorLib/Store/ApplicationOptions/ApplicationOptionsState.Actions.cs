namespace Luthetus.Common.RazorLib.Store.ApplicationOptions;

public partial class AppOptionsState
{
    public record SetAppOptionsStateAction( 
        Func<AppOptionsState, AppOptionsState> WithFunc);
}