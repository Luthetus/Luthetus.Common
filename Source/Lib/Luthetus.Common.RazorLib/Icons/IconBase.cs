using Luthetus.Common.RazorLib.Store.ApplicationOptions;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.Icons;

public class IconBase : FluxorComponent
{
    [Inject]
    private IState<AppOptionsState> AppOptionsStateWrap { get; set; } = null!;

    [CascadingParameter(Name = "LuthetusCommonIconWidthOverride")]
    public int? LuthetusCommonIconWidthOverride { get; set; }
    [CascadingParameter(Name = "LuthetusCommonIconHeightOverride")]
    public int? LuthetusCommonIconHeightOverride { get; set; }

    [Parameter]
    public string CssStyleString { get; set; } = string.Empty;

    public int WidthInPixels =>
        LuthetusCommonIconWidthOverride ??
        AppOptionsStateWrap.Value.Options.IconSizeInPixels ??
        AppOptionsState.MINIMUM_ICON_SIZE_IN_PIXELS;

    public int HeightInPixels =>
        LuthetusCommonIconHeightOverride ??
        AppOptionsStateWrap.Value.Options.IconSizeInPixels ??
        AppOptionsState.MINIMUM_ICON_SIZE_IN_PIXELS;
}