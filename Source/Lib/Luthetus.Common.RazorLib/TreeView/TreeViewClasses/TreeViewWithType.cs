namespace Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

/// <summary>
/// Implement the abstract class <see cref="TreeViewWithType{T}"/>
/// in order to make a TreeView.
/// <br/><br/>
/// An abstract class is used because a good deal of customization
/// is required on a per TreeView basis depending on what data type
/// one displays in that TreeView.
/// </summary>
public abstract class TreeViewWithType<T> : TreeViewNoType
{
    public TreeViewWithType(
        T? item,
        bool isExpandable,
        bool isExpanded)
    {
        Item = item;
        IsExpandable = isExpandable;
        IsExpanded = isExpanded;
    }

    public T? Item { get; }
    public override object? UntypedItem => Item;
    public override Type ItemType => typeof(T);
}