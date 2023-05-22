using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

namespace Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;

/// <summary>
/// <see cref="TreeViewAdhoc"/> is used when the
/// consumer of the component does not want to show the root.
/// <br/><br/>
/// The TreeViews were designed with a root consisting of 1 node.
/// To get around this <see cref="TreeViewAdhoc"/> can be used
/// to have that top level root node be invisible to the user.
/// </summary>
public class TreeViewInterfaceImplementation : TreeViewReflection
{
    private readonly IWatchWindowTreeViewRenderers _watchWindowTreeViewRenderers;

    public TreeViewInterfaceImplementation(
        WatchWindowObjectWrap watchWindowObjectWrap,
        bool isExpandable,
        bool isExpanded,
        IWatchWindowTreeViewRenderers watchWindowTreeViewRenderers)
        : base(
            watchWindowObjectWrap,
            isExpandable,
            isExpanded,
            watchWindowTreeViewRenderers)
    {
        _watchWindowTreeViewRenderers = watchWindowTreeViewRenderers;
    }

    public override TreeViewRenderer GetTreeViewRenderer()
    {
        return new TreeViewRenderer(
            _watchWindowTreeViewRenderers.TreeViewInterfaceImplementationRenderer,
            new Dictionary<string, object?>
            {
                {
                    nameof(TreeViewInterfaceImplementation),
                    this
                },
            });
    }
}