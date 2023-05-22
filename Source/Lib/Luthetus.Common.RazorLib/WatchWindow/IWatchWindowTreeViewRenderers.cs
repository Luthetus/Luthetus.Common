namespace Luthetus.Common.RazorLib.WatchWindow;

public interface IWatchWindowTreeViewRenderers
{
    public Type TreeViewTextRenderer { get; }
    public Type TreeViewReflectionRenderer { get; }
    public Type TreeViewPropertiesRenderer { get; }
    public Type TreeViewInterfaceImplementationRenderer { get; }
    public Type TreeViewFieldsRenderer { get; }
    public Type TreeViewExceptionRenderer { get; }
    public Type TreeViewEnumerableRenderer { get; }
}