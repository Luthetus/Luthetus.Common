using Luthetus.Common.RazorLib.TreeView.Commands;
using Luthetus.Common.RazorLib.TreeView.Events;
using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Luthetus.Common.RazorLib.TreeView.Displays;

public partial class TreeViewDisplay : ComponentBase
{
    [Inject]
    private ITreeViewService TreeViewService { get; set; } = null!;

    [CascadingParameter]
    public TreeViewState TreeViewState { get; set; } = null!;
    [CascadingParameter(Name = "HandleTreeViewOnContextMenu")]
    public Func<
        MouseEventArgs, TreeViewState?, TreeViewNoType?, Task>
        HandleTreeViewOnContextMenu
    { get; set; } = null!;
    [CascadingParameter(Name = "TreeViewMouseEventHandler")]
    public TreeViewMouseEventHandler TreeViewMouseEventHandler { get; set; } = null!;
    [CascadingParameter(Name = "TreeViewKeyboardEventHandler")]
    public TreeViewKeyboardEventHandler TreeViewKeyboardEventHandler { get; set; } = null!;
    [CascadingParameter(Name = "OffsetPerDepthInPixels")]
    public int OffsetPerDepthInPixels { get; set; } = 12;
    [CascadingParameter(Name = "LuthetusTreeViewIconWidth")]
    public int WidthOfTitleExpansionChevron { get; set; } = 16;

    [Parameter, EditorRequired]
    public TreeViewNoType TreeViewNoType { get; set; } = null!;
    [Parameter, EditorRequired]
    public int Depth { get; set; }

    private ElementReference? _treeViewTitleElementReference;
    private TreeViewChangedKey _previousTreeViewChangedKey = TreeViewChangedKey.Empty;
    private bool _previousIsActive;

    private int OffsetInPixels => OffsetPerDepthInPixels * Depth;

    private bool IsSelected => TreeViewState.SelectedNodes
        .Any(x => x.TreeViewNodeKey == TreeViewNoType.TreeViewNodeKey);

    private bool IsActive => TreeViewState.ActiveNode is not null &&
                             TreeViewState.ActiveNode.TreeViewNodeKey == TreeViewNoType.TreeViewNodeKey;

    private string IsSelectedCssClass => IsSelected
        ? "luth_selected"
        : string.Empty;

    private string IsActiveCssClass => IsActive
        ? "luth_active"
        : string.Empty;

    protected override bool ShouldRender()
    {
        // TreeViewChangedKey has changed
        {
            if (_previousTreeViewChangedKey != TreeViewNoType.TreeViewChangedKey)
            {
                _previousTreeViewChangedKey = TreeViewNoType.TreeViewChangedKey;
                return true;
            }
        }

        return false;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        var localIsActive = IsActive;

        if (_previousIsActive != localIsActive)
        {
            _previousIsActive = localIsActive;

            if (localIsActive)
                await FocusAsync();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task FocusAsync()
    {
        try
        {
            var localTreeViewTitleElementReference = _treeViewTitleElementReference;

            if (localTreeViewTitleElementReference is not null)
                await localTreeViewTitleElementReference.Value.FocusAsync();
        }
        catch (Exception)
        {
            // 2023-04-18: The app has had a bug where it "freezes" and must be restarted.
            //             This bug is seemingly happening randomly. I have a suspicion
            //             that there are race-condition exceptions occurring with "FocusAsync"
            //             on an ElementReference.
        }
    }

    private async Task HandleExpansionChevronOnMouseDown(
        TreeViewNoType localTreeViewNoType)
    {
        if (!localTreeViewNoType.IsExpandable)
            return;

        localTreeViewNoType.IsExpanded = !localTreeViewNoType.IsExpanded;

        if (localTreeViewNoType.IsExpanded)
            await localTreeViewNoType.LoadChildrenAsync();

        TreeViewService.ReRenderNode(
            TreeViewState.TreeViewStateKey,
            localTreeViewNoType);
    }

    /// <summary>
    /// Capture the variables on event as to
    /// avoid referenced object mutating during the event.
    /// </summary>
    private async Task ManuallyPropagateOnContextMenu(
        MouseEventArgs mouseEventArgs,
        TreeViewState? treeViewState,
        TreeViewNoType treeViewNoType)
    {
        var treeViewCommandParameter = new TreeViewCommandParameter(
            TreeViewService,
            TreeViewState,
            TreeViewNoType,
            FocusAsync,
            null,
            mouseEventArgs,
            null);

        _ = await TreeViewMouseEventHandler.OnMouseDownAsync(
            treeViewCommandParameter);

        await HandleTreeViewOnContextMenu.Invoke(
            mouseEventArgs,
            treeViewState,
            treeViewNoType);
    }

    private async Task HandleOnClickAsync(
        MouseEventArgs? mouseEventArgs)
    {
        var treeViewCommandParameter = new TreeViewCommandParameter(
            TreeViewService,
            TreeViewState,
            TreeViewNoType,
            FocusAsync,
            null,
            mouseEventArgs,
            null);

        _ = await TreeViewMouseEventHandler.OnClickAsync(
            treeViewCommandParameter);
    }

    private async Task HandleOnDoubleClick(
        MouseEventArgs? mouseEventArgs)
    {
        var treeViewCommandParameter = new TreeViewCommandParameter(
            TreeViewService,
            TreeViewState,
            TreeViewNoType,
            FocusAsync,
            null,
            mouseEventArgs,
            null);

        _ = await TreeViewMouseEventHandler.OnDoubleClickAsync(
            treeViewCommandParameter);
    }

    private async Task HandleOnMouseDown(
        MouseEventArgs? mouseEventArgs)
    {
        var treeViewCommandParameter = new TreeViewCommandParameter(
            TreeViewService,
            TreeViewState,
            TreeViewNoType,
            FocusAsync,
            null,
            mouseEventArgs,
            null);

        _ = await TreeViewMouseEventHandler.OnMouseDownAsync(
            treeViewCommandParameter);
    }

    private async Task HandleOnKeyDown(
        KeyboardEventArgs keyboardEventArgs,
        TreeViewState treeViewState,
        TreeViewNoType treeViewNoType)
    {
        if (keyboardEventArgs.Key == "ContextMenu")
        {
            var mouseEventArgs = new MouseEventArgs
            {
                Button = -1
            };

            await ManuallyPropagateOnContextMenu(
                mouseEventArgs,
                TreeViewState,
                TreeViewNoType);
        }
    }
}