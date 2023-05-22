using Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.WatchWindow.TreeViewDisplays;

public partial class TreeViewReflectionDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public TreeViewReflection TreeViewReflection { get; set; } = null!;

    private string GetCssStylingForValue(Type itemType)
    {
        if (itemType == typeof(string))
        {
            return "bte_string-literal";
        }
        else if (itemType == typeof(bool))
        {
            return "bte_keyword";
        }
        else
        {
            return string.Empty;
        }
    }
}