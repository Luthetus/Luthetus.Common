using Luthetus.Common.RazorLib.JavaScriptObjects;
using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;
using Microsoft.AspNetCore.Components.Web;

namespace Luthetus.Common.RazorLib.TreeView.Commands;

public interface ITreeViewCommandParameter
{
    public ITreeViewService TreeViewService { get; }
    public TreeViewState TreeViewState { get; }
    public TreeViewNoType? TargetNode { get; }
    public Func<Task> RestoreFocusToTreeView { get; }
    public ContextMenuFixedPosition? ContextMenuFixedPosition { get; }
    public MouseEventArgs? MouseEventArgs { get; }
    public KeyboardEventArgs? KeyboardEventArgs { get; }
}