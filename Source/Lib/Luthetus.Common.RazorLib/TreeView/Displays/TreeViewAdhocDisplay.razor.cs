using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.TreeView.Displays;

public partial class TreeViewAdhocDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public TreeViewNoType TreeViewNoTypeAdhoc { get; set; } = null!;
}