using System.Collections.Immutable;
using Fluxor;
using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

namespace Luthetus.Common.RazorLib.Store.TreeViewCase;

[FeatureState]
public partial class TreeViewStateContainer
{
    public TreeViewStateContainer()
    {
        TreeViewStatesList = ImmutableList<TreeViewState>.Empty;
    }

    public ImmutableList<TreeViewState> TreeViewStatesList { get; set; }
}