using System.Text;
using Fluxor;
using Luthetus.Common.RazorLib.Dialog;
using Luthetus.Common.RazorLib.Html;
using Luthetus.Common.RazorLib.Store.ApplicationOptions;
using Luthetus.Common.RazorLib.Store.DialogCase;
using Luthetus.Common.RazorLib.Store.NotificationCase;
using Luthetus.Common.RazorLib.Dimensions;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.Notification;

public partial class NotificationDisplay : ComponentBase, IDisposable
{
    [Inject]
    private IState<AppOptionsState> AppOptionsStateWrap { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public NotificationRecord NotificationRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public int Index { get; set; }

    private const int WIDTH_IN_PIXELS = 350;
    private const int HEIGHT_IN_PIXELS = 125;
    private const int RIGHT_OFFSET_IN_PIXELS = 15;
    private const int BOTTOM_OFFSET_IN_PIXELS = 15;

    private const int COUNT_OF_CONTROL_BUTTONS = 2;

    private readonly CancellationTokenSource _notificationOverlayCancellationTokenSource = new();
    private readonly DialogKey _dialogKey = DialogKey.NewDialogKey();

    private string CssStyleString => GetCssStyleString();

    private string IconSizeInPixelsCssValue =>
        $"{AppOptionsStateWrap.Value.Options.IconSizeInPixels!.Value.ToCssValue()}";

    private string NotificationTitleCssStyleString =>
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

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var notificationRecord = NotificationRecord;

            if (notificationRecord.NotificationOverlayLifespan is not null)
            {
                // ICommonBackgroundTaskQueue does not work well here because
                // this Task does not need to be tracked.
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await Task.Delay(
                            notificationRecord.NotificationOverlayLifespan.Value,
                            _notificationOverlayCancellationTokenSource.Token);

                        DisposeNotification();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }, CancellationToken.None);
            }
        }

        return base.OnAfterRenderAsync(firstRender);
    }

    private string GetCssStyleString()
    {
        var styleBuilder = new StringBuilder();

        var widthInPixelsInvariantCulture = WIDTH_IN_PIXELS
            .ToCssValue();

        var heightInPixelsInvariantCulture = HEIGHT_IN_PIXELS
            .ToCssValue();

        styleBuilder.Append($" width: {widthInPixelsInvariantCulture}px;");
        styleBuilder.Append($" height: {heightInPixelsInvariantCulture}px;");

        var rightOffsetInPixelsInvariantCulture = RIGHT_OFFSET_IN_PIXELS
            .ToCssValue();

        styleBuilder.Append($" right: {rightOffsetInPixelsInvariantCulture}px;");

        var bottomOffsetDueToHeight = HEIGHT_IN_PIXELS * Index;

        var bottomOffsetDueToBottomOffset = BOTTOM_OFFSET_IN_PIXELS * (1 + Index);

        var totalBottomOffset = bottomOffsetDueToHeight +
                                bottomOffsetDueToBottomOffset;

        var totalBottomOffsetInvariantCulture = totalBottomOffset
            .ToCssValue();

        styleBuilder.Append($" bottom: {totalBottomOffsetInvariantCulture}px;");

        return styleBuilder.ToString();
    }

    private void DisposeNotification()
    {
        Dispatcher.Dispatch(
            new NotificationRecordsCollection.DisposeAction(
                NotificationRecord.NotificationKey));
    }

    private void ChangeNotificationToDialog()
    {
        var dialogRecord = new DialogRecord(
            _dialogKey,
            NotificationRecord.Title,
            NotificationRecord.RendererType,
            NotificationRecord.Parameters,
            NotificationRecord.CssClassString)
        {
            IsResizable = true
        };

        Dispatcher.Dispatch(
            new DialogRecordsCollection.RegisterAction(
                dialogRecord));

        DisposeNotification();
    }

    public void Dispose()
    {
        _notificationOverlayCancellationTokenSource.Cancel();

        AppOptionsStateWrap.StateChanged -= AppOptionsStateWrapOnStateChanged;
    }
}