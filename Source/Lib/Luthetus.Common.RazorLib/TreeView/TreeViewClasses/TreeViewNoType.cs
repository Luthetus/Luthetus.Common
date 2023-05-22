namespace Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

/// <summary>
/// <see cref="IndexAmongSiblings"/> refers to in the Parent tree view entries list of
/// children at what index does this tree view exist at?
/// <br/><br/>
/// The goal of this datatype is to allow for storage of
/// <see cref="TreeViewWithType{T}"/> implementation instances within a centralized
/// store.
/// <br/><br/>
/// Without this datatype one cannot for example
/// hold all their <see cref="TreeViewWithType{T}"/> in a <see cref="List{T}"/> unless
/// all implementation instances share the same generic argument type.
/// </summary>
public abstract class TreeViewNoType
{
    public abstract object? UntypedItem { get; }
    public abstract Type ItemType { get; }

    public TreeViewNoType? Parent { get; set; }
    public List<TreeViewNoType> Children { get; set; } = new();
    public int IndexAmongSiblings { get; set; }
    public bool IsRoot { get; set; }
    public bool IsHidden { get; set; }
    public bool IsExpandable { get; set; }
    public bool IsExpanded { get; set; }
    public TreeViewChangedKey TreeViewChangedKey { get; set; } = TreeViewChangedKey.NewTreeViewChangedKey();
    public TreeViewNodeKey TreeViewNodeKey { get; set; } = TreeViewNodeKey.NewTreeViewNodeKey();

    public abstract TreeViewRenderer GetTreeViewRenderer();
    public abstract Task LoadChildrenAsync();

    /// <summary>
    /// <see cref="RemoveRelatedFilesFromParent"/> is used for showing
    /// codebehinds such that a file on the filesystem can be
    /// displayed as having children in the TreeView.
    /// <br/><br/>
    /// In the case of a directory loading its children.
    /// After the directory loads all its children it will loop
    /// through the children invoking <see cref="RemoveRelatedFilesFromParent"/>
    /// on each of the children.
    /// <br/><br/>
    /// If the directory has children 'Component.razor' and 'Component.razor.cs'
    /// then 'Component.razor' will remove 'Component.razor.cs' from the parent
    /// directories children and mark itself as expandable as it saw a related
    /// file in its parent.
    /// </summary>
    public virtual void RemoveRelatedFilesFromParent(List<TreeViewNoType> siblingsAndSelfTreeViews)
    {
        // The default implementation of this method is to do nothing.
        // Override this method to implement some functionality if desired.
    }
}