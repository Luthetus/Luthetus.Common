using Luthetus.Common.RazorLib.TreeView;
using Luthetus.Common.RazorLib.TreeView.Commands;

namespace Luthetus.Common.RazorLib.TreeView.Events;

/// <summary>
/// To implement custom MouseEvent handling logic one should
/// inherit <see cref="TreeViewMouseEventHandler"/> and override
/// the corresponding method.
/// </summary>
public class TreeViewMouseEventHandler
{
    private readonly ITreeViewService _treeViewService;

    public TreeViewMouseEventHandler(
        ITreeViewService treeViewService)
    {
        _treeViewService = treeViewService;
    }

    /// <summary>
    /// -Used for handing "onclick" events within the user interface<br/>
    /// -The base implementation sets the Active TreeView Node
    /// to the Node which was clicked on the UI.
    /// </summary>
    /// <returns>
    /// -True if an action was performed<br/>
    /// -False if an action was not performed
    /// </returns>
    public virtual Task<bool> OnClickAsync(
        ITreeViewCommandParameter treeViewCommandParameter)
    {
        if (treeViewCommandParameter.MouseEventArgs is not null &&
            treeViewCommandParameter.MouseEventArgs.CtrlKey &&
            treeViewCommandParameter.TargetNode is not null)
        {
            _treeViewService.AddSelectedNode(
                treeViewCommandParameter.TreeViewState.TreeViewStateKey,
                treeViewCommandParameter.TargetNode);

            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    /// <summary>
    /// Used for handing "ondblclick" events within the user interface
    /// <br/><br/>
    /// The base implementation does nothing.
    /// </summary>
    /// <returns>
    /// -True if an action was performed<br/>
    /// -False if an action was not performed
    /// </returns>
    public virtual Task<bool> OnDoubleClickAsync(
        ITreeViewCommandParameter treeViewCommandParameter)
    {
        return Task.FromResult(false);
    }

    /// <summary>
    /// Used for handing "onmousedown" events within the user interface
    /// <br/><br/>
    /// The base implementation does nothing.
    /// </summary>
    /// <returns>
    /// -True if an action was performed<br/>
    /// -False if an action was not performed
    /// </returns>
    public virtual Task<bool> OnMouseDownAsync(
        ITreeViewCommandParameter treeViewCommandParameter)
    {
        if (treeViewCommandParameter.TargetNode is null)
            return Task.FromResult(false);

        _treeViewService.SetActiveNode(
            treeViewCommandParameter.TreeViewState.TreeViewStateKey,
            treeViewCommandParameter.TargetNode);

        // Cases where one should not clear the selected nodes
        {
            // { "Ctrl" + "LeftMouseButton" } => MultiSelection;
            if (treeViewCommandParameter.MouseEventArgs is not null &&
                treeViewCommandParameter.MouseEventArgs.CtrlKey &&
                treeViewCommandParameter.TargetNode is not null)
            {
                return Task.FromResult(true);
            }

            // { "LeftMouseButton" } => ContextMenu; &&
            // TargetNode is selected
            if (treeViewCommandParameter.MouseEventArgs is not null &&
                (treeViewCommandParameter.MouseEventArgs.Buttons & 1) != 1 &&
                treeViewCommandParameter.TargetNode is not null &&
                treeViewCommandParameter.TreeViewState.SelectedNodes
                    .Any(x =>
                        x.TreeViewNodeKey == treeViewCommandParameter.TargetNode.TreeViewNodeKey))
            {
                // Not pressing the left mouse button
                // so assume ContextMenu is desired result.
                return Task.FromResult(true);
            }
        }

        _treeViewService.ClearSelectedNodes(
            treeViewCommandParameter.TreeViewState.TreeViewStateKey);

        return Task.FromResult(true);
    }
}