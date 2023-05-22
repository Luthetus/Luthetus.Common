using Luthetus.Common.RazorLib.Store.TreeViewCase;
using Fluxor;
using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

namespace Luthetus.Common.RazorLib.TreeView;

public interface ITreeViewService : ILuthetusCommonService
{
    public IState<TreeViewStateContainer> TreeViewStateContainerWrap { get; }

    /// <summary>
    /// If a <see cref="TreeViewState"/> with the
    /// <see cref="TreeViewStateKey"/> that exists on the
    /// provided parameter <see cref="treeViewState"/>
    /// nothing will occur (no duplicate key exceptions will occur).
    /// </summary>
    public void RegisterTreeViewState(
        TreeViewState treeViewState);

    public void SetRoot(
        TreeViewStateKey treeViewStateKey,
        TreeViewNoType treeViewNoType);

    public bool TryGetTreeViewState(
        TreeViewStateKey treeViewStateKey,
        out TreeViewState? treeViewState);

    public void ReRenderNode(
        TreeViewStateKey treeViewStateKey,
        TreeViewNoType node);

    public void AddChildNode(
        TreeViewStateKey treeViewStateKey,
        TreeViewNoType parent,
        TreeViewNoType child);

    public void SetActiveNode(
        TreeViewStateKey treeViewStateKey,
        TreeViewNoType? nextActiveNode);

    public void AddSelectedNode(
        TreeViewStateKey treeViewStateKey,
        TreeViewNoType nodeSelection);

    public void RemoveSelectedNode(
        TreeViewStateKey treeViewStateKey,
        TreeViewNodeKey treeViewNodeKey);

    public void ClearSelectedNodes(
        TreeViewStateKey treeViewStateKey);

    public void MoveLeft(
        TreeViewStateKey treeViewStateKey,
        bool shiftKey);

    public void MoveDown(
        TreeViewStateKey treeViewStateKey,
        bool shiftKey);

    public void MoveUp(
        TreeViewStateKey treeViewStateKey,
        bool shiftKey);

    public void MoveRight(
        TreeViewStateKey treeViewStateKey,
        bool shiftKey);

    public void MoveHome(
        TreeViewStateKey treeViewStateKey,
        bool shiftKey);

    public void MoveEnd(
        TreeViewStateKey treeViewStateKey,
        bool shiftKey);

    public string GetNodeElementId(
        TreeViewNoType treeViewNoType);

    public string GetTreeContainerElementId(
        TreeViewStateKey treeViewStateKey);

    public void DisposeTreeViewState(
        TreeViewStateKey treeViewStateKey);
}