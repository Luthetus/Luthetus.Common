using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

namespace Luthetus.Common.RazorLib.Store.TreeViewCase;

public partial class TreeViewStateContainer
{
    public record RegisterTreeViewStateAction( 
        TreeViewState TreeViewState);
    
    public record ReplaceTreeViewStateAction( 
        TreeViewStateKey TreeViewStateKey,
        TreeViewState TreeViewState);
    
    public record WithRootAction(
        TreeViewStateKey TreeViewStateKey,
        TreeViewNoType TreeViewNoType);
    
    public record TryGetRootAction(
        TreeViewStateKey TreeViewStateKey);
    
    public record AddChildNodeAction(
        TreeViewStateKey TreeViewStateKey,
        TreeViewNoType Parent,
        TreeViewNoType Child);
    
    public record ReRenderSpecifiedNodeAction(
        TreeViewStateKey TreeViewStateKey,
        TreeViewNoType Node);
    
    public record SetActiveNodeAction(
        TreeViewStateKey TreeViewStateKey,
        TreeViewNoType? NextActiveNode);
    
    public record AddSelectedNodeAction(
        TreeViewStateKey TreeViewStateKey,
        TreeViewNoType NodeSelection);
    
    public record RemoveSelectedNodeAction(
        TreeViewStateKey TreeViewStateKey,
        TreeViewNodeKey TreeViewNodeKey);
    
    /// <summary>
    /// If a movement is performed on the TreeView without
    /// the "ShiftKey" being held. Then the selected nodes
    /// are cleared.
    /// </summary>
    public record ClearSelectedNodesAction(
        TreeViewStateKey TreeViewStateKey);
    
    public record MoveLeftAction(
        TreeViewStateKey TreeViewStateKey,
        bool ShiftKey);
    
    public record MoveDownAction(
        TreeViewStateKey TreeViewStateKey,
        bool ShiftKey);
    
    public record MoveUpAction(
        TreeViewStateKey TreeViewStateKey,
        bool ShiftKey);
    
    public record MoveRightAction(
        TreeViewStateKey TreeViewStateKey,
        bool ShiftKey,
        Action<TreeViewNoType> LoadChildrenAction);
    
    public record MoveHomeAction(
        TreeViewStateKey TreeViewStateKey,
        bool ShiftKey);
    
    public record MoveEndAction(
        TreeViewStateKey TreeViewStateKey,
        bool ShiftKey);
    
    public record LoadChildrenAction(
        TreeViewStateKey TreeViewStateKey,
        TreeViewNoType TreeViewNoType);
    
    public record DisposeTreeViewStateAction(
        TreeViewStateKey TreeViewStateKey);
}