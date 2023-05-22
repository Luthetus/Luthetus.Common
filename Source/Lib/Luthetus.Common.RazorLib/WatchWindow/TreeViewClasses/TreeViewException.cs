using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

namespace Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;

public class TreeViewException : TreeViewWithType<Exception>
{
    private readonly IWatchWindowTreeViewRenderers _watchWindowTreeViewRenderers;

    public TreeViewException(
        Exception exception,
        bool isExpandable,
        bool isExpanded,
        IWatchWindowTreeViewRenderers watchWindowTreeViewRenderers)
        : base(
            exception,
            isExpandable,
            isExpanded)
    {
        _watchWindowTreeViewRenderers = watchWindowTreeViewRenderers;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null ||
            obj is not TreeViewException treeViewException)
        {
            return false;
        }

        return treeViewException.Item == Item;
    }

    public override int GetHashCode()
    {
        return Item?.GetHashCode() ?? default;
    }

    public override TreeViewRenderer GetTreeViewRenderer()
    {
        return new TreeViewRenderer(
            _watchWindowTreeViewRenderers.TreeViewExceptionRenderer,
            new Dictionary<string, object?>
            {
                {
                    nameof(TreeViewException),
                    this
                },
            });
    }

    public override Task LoadChildrenAsync()
    {
        return Task.CompletedTask;
    }
}