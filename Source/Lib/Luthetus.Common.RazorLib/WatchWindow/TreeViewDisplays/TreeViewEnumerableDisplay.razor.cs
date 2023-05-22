using Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.WatchWindow.TreeViewDisplays;

public partial class TreeViewEnumerableDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public TreeViewEnumerable TreeViewEnumerable { get; set; } = null!;
}