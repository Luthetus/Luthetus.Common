using Luthetus.Common.RazorLib.BackgroundTaskCase;

namespace Luthetus.Common.RazorLib.ComponentRenderers.Types;

public interface IBackgroundTaskDisplayRendererType
{
    public IBackgroundTask BackgroundTask { get; set; }
}