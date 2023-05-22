using Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.WatchWindow.TreeViewDisplays;

public partial class TreeViewFieldsDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public TreeViewFields TreeViewFields { get; set; } = null!;
}