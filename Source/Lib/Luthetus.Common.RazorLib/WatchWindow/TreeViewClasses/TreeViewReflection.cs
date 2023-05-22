using System.Collections;
using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

namespace Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;

public class TreeViewReflection : TreeViewWithType<WatchWindowObjectWrap>
{
    private readonly IWatchWindowTreeViewRenderers _watchWindowTreeViewRenderers;

    public TreeViewReflection(
        WatchWindowObjectWrap watchWindowObjectWrap,
        bool isExpandable,
        bool isExpanded,
        IWatchWindowTreeViewRenderers watchWindowTreeViewRenderers)
        : base(
            watchWindowObjectWrap,
            isExpandable,
            isExpanded)
    {
        _watchWindowTreeViewRenderers = watchWindowTreeViewRenderers;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null ||
            obj is not TreeViewReflection treeViewReflection)
        {
            return false;
        }

        return treeViewReflection.Item == Item;
    }

    public override int GetHashCode()
    {
        return Item?.GetHashCode() ?? default;
    }

    public override TreeViewRenderer GetTreeViewRenderer()
    {
        return new TreeViewRenderer(
            _watchWindowTreeViewRenderers.TreeViewReflectionRenderer,
            new Dictionary<string, object?>
            {
                {
                    nameof(TreeViewReflection),
                    this
                },
            });
    }

    public override Task LoadChildrenAsync()
    {
        if (Item is null)
            throw new ApplicationException("Node was null");

        var oldChildrenMap = Children
            .ToDictionary(child => child);

        try
        {
            Children.Clear();

            Children.Add(new TreeViewFields(
                Item,
                true,
                false,
                _watchWindowTreeViewRenderers));

            Children.Add(new TreeViewProperties(
                Item,
                true,
                false,
                _watchWindowTreeViewRenderers));

            if (Item.DebugObjectItem is IEnumerable)
            {
                Children.Add(new TreeViewEnumerable(Item,
                    true,
                    false,
                    _watchWindowTreeViewRenderers));
            }

            if (Item.DebugObjectItemType.IsInterface &&
                Item.DebugObjectItem is not null)
            {
                var interfaceImplementation = new WatchWindowObjectWrap(
                    Item.DebugObjectItem,
                    Item.DebugObjectItem.GetType(),
                    "InterfaceImplementation",
                    false);

                Children.Add(new TreeViewInterfaceImplementation(
                    interfaceImplementation,
                    true,
                    false,
                    _watchWindowTreeViewRenderers));
            }
        }
        catch (Exception e)
        {
            Children.Clear();
            Children.Add(new TreeViewException(
                e,
                false,
                false,
                _watchWindowTreeViewRenderers));
        }

        for (int i = 0; i < Children.Count; i++)
        {
            var child = Children[i];

            child.Parent = this;
            child.IndexAmongSiblings = i;
        }

        foreach (var newChild in Children)
        {
            if (oldChildrenMap.TryGetValue(newChild, out var oldChild))
            {
                newChild.IsExpanded = oldChild.IsExpanded;
                newChild.IsExpandable = oldChild.IsExpandable;
                newChild.IsHidden = oldChild.IsHidden;
                newChild.TreeViewNodeKey = oldChild.TreeViewNodeKey;
                newChild.Children = oldChild.Children;
            }
        }

        return Task.CompletedTask;
    }
}