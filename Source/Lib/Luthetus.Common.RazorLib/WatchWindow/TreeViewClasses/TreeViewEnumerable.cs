using System.Collections;
using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

namespace Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;

public class TreeViewEnumerable : TreeViewWithType<WatchWindowObjectWrap>
{
    private readonly IWatchWindowTreeViewRenderers _watchWindowTreeViewRenderers;

    public TreeViewEnumerable(
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
            obj is not TreeViewEnumerable treeViewEnumerable)
        {
            return false;
        }

        return treeViewEnumerable.Item == Item;
    }

    public override int GetHashCode()
    {
        return Item?.GetHashCode() ?? default;
    }

    public override TreeViewRenderer GetTreeViewRenderer()
    {
        return new TreeViewRenderer(
            _watchWindowTreeViewRenderers.TreeViewEnumerableRenderer,
            new Dictionary<string, object?>
            {
                {
                    nameof(TreeViewEnumerable),
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

            if (Item.DebugObjectItem is IEnumerable enumerable)
            {
                var enumerator = enumerable.GetEnumerator();

                var genericArgument = GetGenericArgument(Item.DebugObjectItem.GetType());

                while (enumerator.MoveNext())
                {
                    var entry = enumerator.Current;

                    var childNode = new WatchWindowObjectWrap(
                        entry,
                        genericArgument,
                        genericArgument.Name,
                        Item.IsPubliclyReadable);

                    Children.Add(new TreeViewReflection(
                        childNode,
                        true,
                        false,
                        _watchWindowTreeViewRenderers));
                }
            }
            else
            {
                throw new ApplicationException(
                    $"Unexpected failed cast to the Type {nameof(IEnumerable)}." +
                    $" {nameof(TreeViewEnumerable)} are to have a {nameof(Item.DebugObjectItem)} which is castable as {nameof(IEnumerable)}");
            }

            if (Children.Count == 0)
            {
                Children.Add(new TreeViewText(
                    "Enumeration returned no results.",
                    false,
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

    // https://stackoverflow.com/questions/906499/getting-type-t-from-ienumerablet
    private static Type GetGenericArgument(Type type)
    {
        // Type is Array
        // short-circuit if you expect lots of arrays 
        if (type.IsArray)
            return type.GetElementType()!;

        // type is IEnumerable<T>;
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            return type.GetGenericArguments()[0];

        // type implements/extends IEnumerable<T>;
        var enumType = type.GetInterfaces()
            .Where(t => t.IsGenericType &&
                        t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            .Select(t => t.GenericTypeArguments[0]).FirstOrDefault();
        return enumType ?? type;
    }
}