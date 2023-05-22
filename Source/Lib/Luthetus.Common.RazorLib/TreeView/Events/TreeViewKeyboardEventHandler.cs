using Luthetus.Common.RazorLib.Keyboard;
using Luthetus.Common.RazorLib.TreeView.Commands;

namespace Luthetus.Common.RazorLib.TreeView.Events;

/// <summary>
/// To implement custom KeyboardEvent handling logic one should
/// inherit <see cref="TreeViewKeyboardEventHandler"/> and override
/// the corresponding method.
/// </summary>
public class TreeViewKeyboardEventHandler
{
    private readonly ITreeViewService _treeViewService;

    public TreeViewKeyboardEventHandler(
        ITreeViewService treeViewService)
    {
        _treeViewService = treeViewService;
    }

    /// <summary>
    /// -Used for handing "onkeydownwithpreventscroll" events within the user interface<br/>
    /// </summary>
    /// <returns>
    /// -True if the keydown mapped to an action<br/>
    /// -False if the keydown did NOT map to an action
    /// </returns>
    public virtual Task<bool> OnKeyDownAsync(
        ITreeViewCommandParameter treeViewCommandParameter)
    {
        if (treeViewCommandParameter.KeyboardEventArgs is null)
            return Task.FromResult(false);

        var inputMappedToAnAction = true;

        switch (treeViewCommandParameter.KeyboardEventArgs.Key)
        {
            case KeyboardKeyFacts.MovementKeys.ARROW_LEFT:
                _treeViewService.MoveLeft(
                    treeViewCommandParameter.TreeViewState.TreeViewStateKey,
                    treeViewCommandParameter.KeyboardEventArgs.ShiftKey);
                break;
            case KeyboardKeyFacts.MovementKeys.ARROW_DOWN:
                _treeViewService.MoveDown(
                    treeViewCommandParameter.TreeViewState.TreeViewStateKey,
                    treeViewCommandParameter.KeyboardEventArgs.ShiftKey);
                break;
            case KeyboardKeyFacts.MovementKeys.ARROW_UP:
                _treeViewService.MoveUp(
                    treeViewCommandParameter.TreeViewState.TreeViewStateKey,
                    treeViewCommandParameter.KeyboardEventArgs.ShiftKey);
                break;
            case KeyboardKeyFacts.MovementKeys.ARROW_RIGHT:
                _treeViewService.MoveRight(
                    treeViewCommandParameter.TreeViewState.TreeViewStateKey,
                    treeViewCommandParameter.KeyboardEventArgs.ShiftKey);
                break;
            case KeyboardKeyFacts.MovementKeys.HOME:
                _treeViewService.MoveHome(
                    treeViewCommandParameter.TreeViewState.TreeViewStateKey,
                    treeViewCommandParameter.KeyboardEventArgs.ShiftKey);
                break;
            case KeyboardKeyFacts.MovementKeys.END:
                _treeViewService.MoveEnd(
                    treeViewCommandParameter.TreeViewState.TreeViewStateKey,
                    treeViewCommandParameter.KeyboardEventArgs.ShiftKey);
                break;
            default:
                inputMappedToAnAction = false;
                break;
        }

        return Task.FromResult(inputMappedToAnAction);
    }
}