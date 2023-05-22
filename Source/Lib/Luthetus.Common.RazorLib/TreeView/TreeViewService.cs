using Fluxor;
using Luthetus.Common.RazorLib.Store.TreeViewCase;
using Luthetus.Common.RazorLib.BackgroundTaskCase;
using Luthetus.Common.RazorLib.ComponentRenderers;
using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

namespace Luthetus.Common.RazorLib.TreeView;

public class TreeViewService : ITreeViewService
{
    private readonly IDispatcher _dispatcher;
    private readonly IBackgroundTaskQueue _backgroundTaskQueue;
    private readonly ILuthetusCommonComponentRenderers _luthetusCommonComponentRenderers;

    public TreeViewService(
        bool isEnabled,
        IState<TreeViewStateContainer> treeViewStateContainerWrap,
        IDispatcher dispatcher,
        IBackgroundTaskQueue backgroundTaskQueue,
        ILuthetusCommonComponentRenderers luthetusCommonComponentRenderers)
    {
        IsEnabled = isEnabled;
        TreeViewStateContainerWrap = treeViewStateContainerWrap;
        _dispatcher = dispatcher;
        _backgroundTaskQueue = backgroundTaskQueue;
        _luthetusCommonComponentRenderers = luthetusCommonComponentRenderers;
    }

    public bool IsEnabled { get; }
    public IState<TreeViewStateContainer> TreeViewStateContainerWrap { get; }

    public void RegisterTreeViewState(TreeViewState treeViewState)
    {
        var registerTreeViewStateAction = new TreeViewStateContainer.RegisterTreeViewStateAction(
            treeViewState);

        _dispatcher.Dispatch(registerTreeViewStateAction);
    }

    public void ReplaceTreeViewState(
        TreeViewStateKey treeViewStateKey,
        TreeViewState treeViewState)
    {
        var replaceTreeViewStateAction = new TreeViewStateContainer.ReplaceTreeViewStateAction(
            treeViewStateKey,
            treeViewState);

        _dispatcher.Dispatch(replaceTreeViewStateAction);
    }

    public void SetRoot(TreeViewStateKey treeViewStateKey, TreeViewNoType treeViewNoType)
    {
        var withRootAction = new TreeViewStateContainer.WithRootAction(
            treeViewStateKey,
            treeViewNoType);

        _dispatcher.Dispatch(withRootAction);
    }

    public bool TryGetTreeViewState(
        TreeViewStateKey treeViewStateKey,
        out TreeViewState? treeViewState)
    {
        var foundTreeViewState = TreeViewStateContainerWrap.Value.TreeViewStatesList
            .FirstOrDefault(x =>
                x.TreeViewStateKey == treeViewStateKey);

        treeViewState = foundTreeViewState;

        // Do not null check treeViewState as the memory location is publicly scoped.  
        return foundTreeViewState is not null;
    }

    public void ReRenderNode(
        TreeViewStateKey treeViewStateKey,
        TreeViewNoType node)
    {
        var replaceNodeAction = new TreeViewStateContainer.ReRenderSpecifiedNodeAction(
            treeViewStateKey,
            node);

        _dispatcher.Dispatch(replaceNodeAction);
    }

    public void AddChildNode(
        TreeViewStateKey treeViewStateKey,
        TreeViewNoType parent,
        TreeViewNoType child)
    {
        var addChildNodeAction = new TreeViewStateContainer.AddChildNodeAction(
            treeViewStateKey,
            parent,
            child);

        _dispatcher.Dispatch(addChildNodeAction);
    }

    public void SetActiveNode(
        TreeViewStateKey treeViewStateKey,
        TreeViewNoType? nextActiveNode)
    {
        var setActiveNodeAction = new TreeViewStateContainer.SetActiveNodeAction(
            treeViewStateKey,
            nextActiveNode);

        _dispatcher.Dispatch(setActiveNodeAction);
    }

    public void AddSelectedNode(
        TreeViewStateKey treeViewStateKey,
        TreeViewNoType nodeSelection)
    {
        var addSelectedNodeAction = new TreeViewStateContainer.AddSelectedNodeAction(
            treeViewStateKey,
            nodeSelection);

        _dispatcher.Dispatch(addSelectedNodeAction);
    }

    public void RemoveSelectedNode(
        TreeViewStateKey treeViewStateKey,
        TreeViewNodeKey treeViewNodeKey)
    {
        var removeSelectedNodeAction = new TreeViewStateContainer.RemoveSelectedNodeAction(
            treeViewStateKey,
            treeViewNodeKey);

        _dispatcher.Dispatch(removeSelectedNodeAction);
    }

    public void ClearSelectedNodes(
        TreeViewStateKey treeViewStateKey)
    {
        var clearSelectedNodesAction = new TreeViewStateContainer.ClearSelectedNodesAction(
            treeViewStateKey);

        _dispatcher.Dispatch(clearSelectedNodesAction);
    }

    public void MoveLeft(
        TreeViewStateKey treeViewStateKey,
        bool shiftKey)
    {
        var moveActiveSelectionLeftAction = new TreeViewStateContainer.MoveLeftAction(
            treeViewStateKey,
            shiftKey);

        _dispatcher.Dispatch(moveActiveSelectionLeftAction);
    }

    public void MoveDown(
        TreeViewStateKey treeViewStateKey,
        bool shiftKey)
    {
        var moveActiveSelectionDownAction = new TreeViewStateContainer.MoveDownAction(
            treeViewStateKey,
            shiftKey);

        _dispatcher.Dispatch(moveActiveSelectionDownAction);
    }

    public void MoveUp(
        TreeViewStateKey treeViewStateKey,
        bool shiftKey)
    {
        var moveActiveSelectionUpAction = new TreeViewStateContainer.MoveUpAction(
            treeViewStateKey,
            shiftKey);

        _dispatcher.Dispatch(moveActiveSelectionUpAction);
    }

    public void MoveRight(
        TreeViewStateKey treeViewStateKey,
        bool shiftKey)
    {
        var moveActiveSelectionRightAction = new TreeViewStateContainer.MoveRightAction(
            treeViewStateKey,
            shiftKey,
            treeViewNoType =>
            {
                // IBackgroundTaskQueue does not work well here because
                // this Task does not need to be tracked.
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await treeViewNoType
                            .LoadChildrenAsync()
                            .ConfigureAwait(false);

                        var reRenderActiveNodeAction = new TreeViewStateContainer.ReRenderSpecifiedNodeAction(
                            treeViewStateKey,
                            treeViewNoType);

                        _dispatcher.Dispatch(reRenderActiveNodeAction);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }, CancellationToken.None);
            });

        _dispatcher.Dispatch(moveActiveSelectionRightAction);
    }

    public void MoveHome(
        TreeViewStateKey treeViewStateKey,
        bool shiftKey)
    {
        var moveActiveSelectionHomeAction = new TreeViewStateContainer.MoveHomeAction(
            treeViewStateKey,
            shiftKey);

        _dispatcher.Dispatch(moveActiveSelectionHomeAction);
    }

    public void MoveEnd(
        TreeViewStateKey treeViewStateKey,
        bool shiftKey)
    {
        var moveActiveSelectionEndAction = new TreeViewStateContainer.MoveEndAction(
            treeViewStateKey,
            shiftKey);

        _dispatcher.Dispatch(moveActiveSelectionEndAction);
    }

    public string GetNodeElementId(
        TreeViewNoType treeViewNoType)
    {
        return $"luth_node-{treeViewNoType.TreeViewNodeKey}";
    }

    public string GetTreeContainerElementId(
        TreeViewStateKey treeViewStateKey)
    {
        return $"luth_tree-container-{treeViewStateKey.Guid}";
    }

    public void DisposeTreeViewState(TreeViewStateKey treeViewStateKey)
    {
        var disposeTreeViewStateAction = new TreeViewStateContainer.DisposeTreeViewStateAction(
            treeViewStateKey);

        _dispatcher.Dispatch(disposeTreeViewStateAction);
    }
}