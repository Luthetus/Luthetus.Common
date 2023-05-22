using Luthetus.Common.RazorLib.Options;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib;

public partial class LuthetusCommonInitializer : ComponentBase
{
    [Inject]
    private LuthetusCommonOptions LuthetusCommonOptions { get; set; } = null!;
    [Inject]
    private IAppOptionsService AppOptionsService { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            AppOptionsService.SetActiveThemeRecordKey(
                LuthetusCommonOptions.InitialThemeKey,
                false);

            await AppOptionsService.SetFromLocalStorageAsync();
        }

        await base.OnAfterRenderAsync(firstRender);
    }
}