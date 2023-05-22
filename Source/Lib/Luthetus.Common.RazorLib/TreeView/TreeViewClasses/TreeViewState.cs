using System.Collections.Immutable;

namespace Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

public record TreeViewState
{
    /// <summary>
    /// If <see cref="rootNode"/> is null then <see cref="TreeViewAdhoc.ConstructTreeViewAdhoc()"/>
    /// will be invoked and the return value will be used as the <see cref="RootNode"/>
    /// </summary>
    public TreeViewState(
        TreeViewStateKey treeViewStateKey,
        TreeViewNoType? rootNode,
        TreeViewNoType? activeNode,
        ImmutableList<TreeViewNoType> selectedNodes)
    {
        rootNode ??= TreeViewAdhoc.ConstructTreeViewAdhoc();

        TreeViewStateKey = treeViewStateKey;
        RootNode = rootNode;
        ActiveNode = activeNode;
        SelectedNodes = selectedNodes;
    }

    public TreeViewStateKey TreeViewStateKey { get; init; }
    public TreeViewNoType RootNode { get; init; }
    public TreeViewNoType? ActiveNode { get; init; }
    /// <summary>
    /// When <see cref="SelectedNodes"/> is Empty, then the <see cref="ActiveNode"/>
    /// will be treated as the single selected node.
    /// </summary>
    public ImmutableList<TreeViewNoType> SelectedNodes { get; init; }
    public Guid StateId { get; init; } = Guid.NewGuid();
}