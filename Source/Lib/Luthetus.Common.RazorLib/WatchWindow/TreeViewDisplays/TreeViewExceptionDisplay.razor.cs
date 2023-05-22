using Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.WatchWindow.TreeViewDisplays;

public partial class TreeViewExceptionDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public TreeViewException TreeViewException { get; set; } = null!;
}