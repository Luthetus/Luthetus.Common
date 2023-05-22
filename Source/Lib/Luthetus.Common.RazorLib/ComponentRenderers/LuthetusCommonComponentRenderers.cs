using Luthetus.Common.RazorLib.WatchWindow;

namespace Luthetus.Common.RazorLib.ComponentRenderers;

public class LuthetusCommonComponentRenderers : ILuthetusCommonComponentRenderers
{
    public LuthetusCommonComponentRenderers(
        Type? backgroundTaskDisplayRendererType,
        Type? errorNotificationRendererType,
        Type? informativeNotificationRendererType,
        Type? treeViewExceptionRendererType,
        Type? treeViewMissingRendererFallbackType,
        IWatchWindowTreeViewRenderers? watchWindowTreeViewRenderers)
    {
        BackgroundTaskDisplayRendererType = backgroundTaskDisplayRendererType;
        ErrorNotificationRendererType = errorNotificationRendererType;
        InformativeNotificationRendererType = informativeNotificationRendererType;
        TreeViewExceptionRendererType = treeViewExceptionRendererType;
        TreeViewMissingRendererFallbackType = treeViewMissingRendererFallbackType;
        WatchWindowTreeViewRenderers = watchWindowTreeViewRenderers;
    }

    public Type? BackgroundTaskDisplayRendererType { get; }
    public Type? ErrorNotificationRendererType { get; }
    public Type? InformativeNotificationRendererType { get; }
    public Type? TreeViewExceptionRendererType { get; }
    public Type? TreeViewMissingRendererFallbackType { get; }
    public IWatchWindowTreeViewRenderers? WatchWindowTreeViewRenderers { get; }
}