namespace Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

public record TreeViewNodeKey(Guid Guid)
{
    public static readonly TreeViewNodeKey Empty = new TreeViewNodeKey(Guid.Empty);

    public static TreeViewNodeKey NewTreeViewNodeKey()
    {
        return new TreeViewNodeKey(Guid.NewGuid());
    }
}