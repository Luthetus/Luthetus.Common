using Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.WatchWindow.TreeViewDisplays;

public partial class TreeViewTextDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public TreeViewText TreeViewText { get; set; } = null!;
}