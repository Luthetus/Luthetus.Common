using Luthetus.Common.RazorLib.WatchWindow;

namespace Luthetus.Common.RazorLib.ComponentRenderers;

public interface ILuthetusCommonComponentRenderers
{
    public Type? BackgroundTaskDisplayRendererType { get; }
    public Type? ErrorNotificationRendererType { get; }
    public Type? InformativeNotificationRendererType { get; }
    public Type? TreeViewExceptionRendererType { get; }
    public Type? TreeViewMissingRendererFallbackType { get; }
    public IWatchWindowTreeViewRenderers? WatchWindowTreeViewRenderers { get; }
}