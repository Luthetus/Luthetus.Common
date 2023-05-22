namespace Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

public record TreeViewRenderer(
    Type DynamicComponentType,
    Dictionary<string, object?> DynamicComponentParameters);