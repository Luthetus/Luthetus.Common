using System.Collections.Immutable;
using Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;
using Fluxor.Blazor.Web.Components;
using Luthetus.Common.RazorLib.ComponentRenderers;
using Luthetus.Common.RazorLib.Dropdown;
using Luthetus.Common.RazorLib.TreeView;
using Luthetus.Common.RazorLib.TreeView.Commands;
using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.WatchWindow;

public partial class WatchWindowDisplay : FluxorComponent
{
    [Inject]
    private ITreeViewService TreeViewService { get; set; } = null!;
    [Inject]
    private IDropdownService DropdownService { get; set; } = null!;
    [Inject]
    private ILuthetusCommonComponentRenderers LuthetusCommonComponentRenderers { get; set; } = null!;

    [Parameter, EditorRequired]
    public WatchWindowObjectWrap WatchWindowObjectWrap { get; set; } = null!;

    public static TreeViewStateKey WatchWindowDisplayTreeViewStateKey { get; } = TreeViewStateKey.NewTreeViewStateKey();
    public static DropdownKey WatchWindowContextMenuDropdownKey { get; } = DropdownKey.NewDropdownKey();

    private ITreeViewCommandParameter? _mostRecentTreeViewContextMenuCommandParameter;
    private bool _disposed;

    protected override async Task OnInitializedAsync()
    {
        if (!TreeViewService.TryGetTreeViewState(
                WatchWindowDisplayTreeViewStateKey,
                out var treeViewState))
        {
            var rootNode = new TreeViewReflection(
                WatchWindowObjectWrap,
                true,
                false,
                LuthetusCommonComponentRenderers.WatchWindowTreeViewRenderers!);

            TreeViewService.RegisterTreeViewState(new TreeViewState(
                WatchWindowDisplayTreeViewStateKey,
                rootNode,
                rootNode,
                ImmutableList<TreeViewNoType>.Empty));
        }

        await base.OnInitializedAsync();
    }

    private async Task OnTreeViewContextMenuFunc(ITreeViewCommandParameter treeViewCommandParameter)
    {
        _mostRecentTreeViewContextMenuCommandParameter = treeViewCommandParameter;

        DropdownService.AddActiveDropdownKey(
            WatchWindowContextMenuDropdownKey);

        await InvokeAsync(StateHasChanged);
    }

    protected override void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            TreeViewService.DisposeTreeViewState(
                WatchWindowDisplayTreeViewStateKey);
        }

        _disposed = true;

        base.Dispose(disposing);
    }
}