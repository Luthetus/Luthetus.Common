using Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.WatchWindow.TreeViewDisplays;

public partial class TreeViewPropertiesDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public TreeViewProperties TreeViewProperties { get; set; } = null!;
}