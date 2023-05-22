using Luthetus.Common.RazorLib.Html;
using Luthetus.Common.RazorLib.Store.ApplicationOptions;
using Fluxor;
using Luthetus.Common.RazorLib.Dimensions;
using Luthetus.Common.RazorLib.Resize;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.Dialog;

public partial class DialogDisplay : IDisposable
{
    [Inject]
    private IDialogService DialogService { get; set; } = null!;
    [Inject]
    private IState<AppOptionsState> AppOptionsStateWrap { get; set; } = null!;

    [Parameter]
    public DialogRecord DialogRecord { get; set; } = null!;

    private const int COUNT_OF_CONTROL_BUTTONS = 2;

    private ResizableDisplay? _resizableDisplay;

    private string ElementDimensionsStyleCssString => DialogRecord.ElementDimensions.StyleString;
    private string IsMaximizedStyleCssString => DialogRecord.IsMaximized
        ? "width: 100vw; height: 100vh; left: 0; top: 0;"
        : string.Empty;

    private string IconSizeInPixelsCssValue =>
        $"{AppOptionsStateWrap.Value.Options.IconSizeInPixels!.Value.ToCssValue()}";

    private string DialogTitleCssStyleString =>
        "width: calc(100% -" +
        $" ({COUNT_OF_CONTROL_BUTTONS} * ({IconSizeInPixelsCssValue}px)) -" +
        $" ({COUNT_OF_CONTROL_BUTTONS} * ({HtmlFacts.Button.ButtonPaddingHorizontalTotalInPixelsCssValue})));";

    protected override void OnInitialized()
    {
        AppOptionsStateWrap.StateChanged += AppOptionsStateWrapOnStateChanged;

        base.OnInitialized();
    }

    private async void AppOptionsStateWrapOnStateChanged(object? sender, EventArgs e)
    {
        await InvokeAsync(StateHasChanged);
    }

    private async Task ReRenderAsync()
    {
        await InvokeAsync(StateHasChanged);
    }

    private void SubscribeMoveHandle()
    {
        _resizableDisplay?.SubscribeToDragEventWithMoveHandle();
    }

    private void ToggleIsMaximized()
    {
        DialogService.SetDialogRecordIsMaximized(
            DialogRecord.DialogKey,
            !DialogRecord.IsMaximized);
    }

    private void DispatchDisposeDialogRecordAction()
    {
        DialogService.DisposeDialogRecord(
            DialogRecord.DialogKey);
    }

    public void Dispose()
    {
        AppOptionsStateWrap.StateChanged -= AppOptionsStateWrapOnStateChanged;
    }
}