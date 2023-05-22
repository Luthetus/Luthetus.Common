using Luthetus.Common.RazorLib.Store.TreeViewCase;
using Luthetus.Common.RazorLib.TreeView.Commands;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Luthetus.Common.RazorLib.Dimensions;
using Luthetus.Common.RazorLib.JavaScriptObjects;
using Luthetus.Common.RazorLib.TreeView.Events;
using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Luthetus.Common.RazorLib.TreeView.Displays;

public partial class TreeViewStateDisplay : FluxorComponent
{
    [Inject]
    private IStateSelection<TreeViewStateContainer, TreeViewState?> TreeViewStateContainerWrap { get; set; } = null!;
    [Inject]
    private ITreeViewService TreeViewService { get; set; } = null!;
    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter, EditorRequired]
    public TreeViewStateKey TreeViewStateKey { get; set; } = null!;
    [Parameter]
    public string CssClassString { get; set; } = string.Empty;
    [Parameter]
    public string CssStyleString { get; set; } = string.Empty;
    [Parameter, EditorRequired]
    public TreeViewMouseEventHandler TreeViewMouseEventHandler { get; set; } = null!;
    [Parameter, EditorRequired]
    public TreeViewKeyboardEventHandler TreeViewKeyboardEventHandler { get; set; } = null!;
    /// <summary>
    /// If a consumer of the TreeView component does not have logic
    /// for their own DropdownComponent then one can provide a
    /// RenderFragment and a dropdown will be rendered for the consumer
    /// then their RenderFragment is rendered within that dropdown.
    /// <br/><br/>
    /// If one has their own DropdownComponent it is recommended
    /// they use <see cref="OnContextMenuFunc"/> instead.
    /// </summary>
    [Parameter]
    public RenderFragment<ITreeViewCommandParameter>? OnContextMenuRenderFragment { get; set; }
    /// <summary>
    /// If a consumer of the TreeView component does not have logic
    /// for their own DropdownComponent it is recommended to use
    /// <see cref="OnContextMenuRenderFragment"/>
    /// <br/><br/>
    /// <see cref="OnContextMenuFunc"/> allows one to be notified of
    /// the ContextMenu event along with the necessary parameters
    /// by being given <see cref="ITreeViewCommandParameter"/>
    /// </summary>
    [Parameter]
    public Func<ITreeViewCommandParameter, Task>? OnContextMenuFunc { get; set; }

    private ITreeViewCommandParameter? _treeViewContextMenuCommandParameter;
    private ElementReference? _treeViewStateDisplayElementReference;

    private string ContextMenuCssStyleString => GetContextMenuCssStyleString();

    protected override void OnInitialized()
    {
        TreeViewStateContainerWrap.Select(tvsc =>
        {
            var treeViewState = tvsc.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == TreeViewStateKey);

            return treeViewState;
        });

        base.OnInitialized();
    }

    private int GetRootDepth(TreeViewNoType rootNode)
    {
        return rootNode is TreeViewAdhoc
            ? -1
            : 0;
    }

    private async Task HandleTreeViewOnKeyDownWithPreventScroll(
        KeyboardEventArgs keyboardEventArgs,
        TreeViewState? treeViewState)
    {
        if (treeViewState is null)
            return;

        var treeViewCommandParameter = new TreeViewCommandParameter(
            TreeViewService,
            treeViewState,
            null,
            async () =>
            {
                _treeViewContextMenuCommandParameter = null;
                await InvokeAsync(StateHasChanged);

                var localTreeViewStateDisplayElementReference = _treeViewStateDisplayElementReference;

                try
                {
                    if (localTreeViewStateDisplayElementReference.HasValue)
                        await localTreeViewStateDisplayElementReference.Value.FocusAsync();
                }
                catch (Exception)
                {
                    // 2023-04-18: The app has had a bug where it "freezes" and must be restarted.
                    //             This bug is seemingly happening randomly. I have a suspicion
                    //             that there are race-condition exceptions occurring with "FocusAsync"
                    //             on an ElementReference.
                }
            },
            null,
            null,
            keyboardEventArgs);

        _ = await TreeViewKeyboardEventHandler
            .OnKeyDownAsync(treeViewCommandParameter);
    }

    private async Task HandleTreeViewOnContextMenu(
        MouseEventArgs? mouseEventArgs,
        TreeViewState? treeViewState,
        TreeViewNoType? treeViewMouseWasOver)
    {
        if (treeViewState is null ||
            mouseEventArgs is null)
        {
            return;
        }

        ContextMenuFixedPosition contextMenuFixedPosition;
        TreeViewNoType contextMenuTargetTreeViewNoType;

        if (mouseEventArgs.Button == -1)
        {
            if (treeViewState.ActiveNode is null)
                return;

            // If dedicated context menu button or shift + F10
            // was pressed as opposed to a mouse RightClick
            //
            // Use JavaScript to determine the ContextMenu position

            contextMenuFixedPosition = await JsRuntime
                .InvokeAsync<ContextMenuFixedPosition>(
                    "luthetusCommon.getTreeViewContextMenuFixedPosition",
                    TreeViewService.GetTreeContainerElementId(treeViewState.TreeViewStateKey),
                    TreeViewService.GetNodeElementId(
                        treeViewState.ActiveNode));

            contextMenuTargetTreeViewNoType = treeViewState.ActiveNode;
        }
        else
        {
            // If a mouse RightClick caused the event then use
            // the MouseEventArgs to determine the ContextMenu position

            if (treeViewMouseWasOver is null)
            {
                // 'whitespace' of the TreeView was right clicked as opposed to
                // a TreeView node and the event should be ignored.
                return;
            }

            contextMenuFixedPosition = new ContextMenuFixedPosition(
                true,
                mouseEventArgs.ClientX,
                mouseEventArgs.ClientY);

            contextMenuTargetTreeViewNoType = treeViewMouseWasOver;
        }

        _treeViewContextMenuCommandParameter = new TreeViewCommandParameter(
            TreeViewService,
            treeViewState,
            contextMenuTargetTreeViewNoType,
            async () =>
            {
                _treeViewContextMenuCommandParameter = null;
                await InvokeAsync(StateHasChanged);

                var localTreeViewStateDisplayElementReference = _treeViewStateDisplayElementReference;

                try
                {
                    if (localTreeViewStateDisplayElementReference.HasValue)
                        await localTreeViewStateDisplayElementReference.Value.FocusAsync();
                }
                catch (Exception)
                {
                    // 2023-04-18: The app has had a bug where it "freezes" and must be restarted.
                    //             This bug is seemingly happening randomly. I have a suspicion
                    //             that there are race-condition exceptions occurring with "FocusAsync"
                    //             on an ElementReference.
                }
            },
            contextMenuFixedPosition,
            mouseEventArgs,
            null);

        if (OnContextMenuFunc is not null)
            await OnContextMenuFunc.Invoke(_treeViewContextMenuCommandParameter);

        await InvokeAsync(StateHasChanged);
    }

    private string GetHasActiveNodeCssClass(TreeViewState? treeViewState)
    {
        if (treeViewState is null ||
            treeViewState.ActiveNode is null)
        {
            return string.Empty;
        }

        return "luth_active";
    }

    private string GetContextMenuCssStyleString()
    {
        if (_treeViewContextMenuCommandParameter?.ContextMenuFixedPosition is null)
        {
            // This css should never get applied as the ContextMenu user interface is
            // wrapped in an if statement that checks for _contextMenuFixedPosition not being null
            //
            // Logic is here to suppress the rightfully so nullable reference warning.
            return "display: none;";
        }

        var left =
            $"left: {_treeViewContextMenuCommandParameter.ContextMenuFixedPosition.LeftPositionInPixels.ToCssValue()}px;";

        var top =
            $"top: {_treeViewContextMenuCommandParameter.ContextMenuFixedPosition.TopPositionInPixels.ToCssValue()}px;";

        return $"{left} {top}";
    }
}