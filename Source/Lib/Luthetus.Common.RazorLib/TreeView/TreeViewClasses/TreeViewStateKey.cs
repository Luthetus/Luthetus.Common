namespace Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

public record TreeViewStateKey(Guid Guid)
{
    public static readonly TreeViewStateKey Empty = new TreeViewStateKey(Guid.Empty);

    public static TreeViewStateKey NewTreeViewStateKey()
    {
        return new TreeViewStateKey(Guid.NewGuid());
    }
}