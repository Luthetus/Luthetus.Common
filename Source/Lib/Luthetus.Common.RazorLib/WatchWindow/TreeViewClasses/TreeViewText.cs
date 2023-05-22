using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

namespace Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;

public class TreeViewText : TreeViewWithType<string>
{
    private readonly IWatchWindowTreeViewRenderers _watchWindowTreeViewRenderers;

    public TreeViewText(
        string text,
        bool isExpandable,
        bool isExpanded,
        IWatchWindowTreeViewRenderers watchWindowTreeViewRenderers)
        : base(
            text,
            isExpandable,
            isExpanded)
    {
        _watchWindowTreeViewRenderers = watchWindowTreeViewRenderers;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null ||
            obj is not TreeViewText treeViewText)
        {
            return false;
        }

        return treeViewText.Item == Item;
    }

    public override int GetHashCode()
    {
        return Item?.GetHashCode() ?? default;
    }

    public override TreeViewRenderer GetTreeViewRenderer()
    {
        return new TreeViewRenderer(
            _watchWindowTreeViewRenderers.TreeViewTextRenderer,
            new Dictionary<string, object?>
            {
                {
                    nameof(TreeViewText),
                    this
                },
            });
    }

    public override Task LoadChildrenAsync()
    {
        return Task.CompletedTask;
    }
}