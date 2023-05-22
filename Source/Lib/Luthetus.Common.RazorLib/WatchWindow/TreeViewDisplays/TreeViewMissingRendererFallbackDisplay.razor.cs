using Luthetus.Common.RazorLib.ComponentRenderers.Types;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.WatchWindow.TreeViewDisplays;

public partial class TreeViewMissingRendererFallbackDisplay
    : ComponentBase, ITreeViewMissingRendererFallbackType
{
    [Parameter, EditorRequired]
    public string DisplayText { get; set; } = string.Empty;
}