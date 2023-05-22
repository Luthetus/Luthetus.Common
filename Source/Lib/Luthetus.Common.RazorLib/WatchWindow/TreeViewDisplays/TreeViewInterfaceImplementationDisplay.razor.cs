using Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.WatchWindow.TreeViewDisplays;

public partial class TreeViewInterfaceImplementationDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public TreeViewInterfaceImplementation TreeViewInterfaceImplementation { get; set; } = null!;
}