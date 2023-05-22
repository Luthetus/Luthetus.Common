using Luthetus.Common.RazorLib.Store.ApplicationOptions;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.Options;

public partial class InputAppIconSize : ComponentBase, IDisposable
{
    [Inject]
    private IAppOptionsService AppOptionsService { get; set; } = null!;

    [Parameter]
    public string CssClassString { get; set; } = string.Empty;
    [Parameter]
    public string CssStyleString { get; set; } = string.Empty;

    private int IconSizeInPixels
    {
        get => AppOptionsService.AppOptionsStateWrap.Value.Options.IconSizeInPixels ??
               AppOptionsState.DEFAULT_ICON_SIZE_IN_PIXELS;
        set
        {
            if (value < AppOptionsState.MINIMUM_ICON_SIZE_IN_PIXELS)
                value = AppOptionsState.MINIMUM_ICON_SIZE_IN_PIXELS;

            AppOptionsService.SetIconSize(value);
        }
    }

    protected override Task OnInitializedAsync()
    {
        AppOptionsService.AppOptionsStateWrap.StateChanged += AppOptionsStateWrapOnStateChanged;

        return base.OnInitializedAsync();
    }

    private async void AppOptionsStateWrapOnStateChanged(object? sender, EventArgs e)
    {
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        AppOptionsService.AppOptionsStateWrap.StateChanged -= AppOptionsStateWrapOnStateChanged;
    }
}