using Luthetus.Common.RazorLib.Theme;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.Options;

public partial class InputAppTheme : IDisposable
{
    [Inject]
    private IThemeRecordsCollectionService ThemeRecordsCollectionService { get; set; } = null!;
    [Inject]
    private IAppOptionsService AppOptionsService { get; set; } = null!;

    [Parameter]
    public string CssClassString { get; set; } = string.Empty;
    [Parameter]
    public string CssStyleString { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        AppOptionsService.AppOptionsStateWrap.StateChanged += AppOptionsStateWrapOnStateChanged;
        ThemeRecordsCollectionService.ThemeRecordsCollectionWrap.StateChanged += ThemeStateWrapOnStateChanged;

        base.OnInitialized();
    }

    private void OnThemeSelectChanged(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value is null)
            return;

        var themeState = ThemeRecordsCollectionService.ThemeRecordsCollectionWrap.Value;

        var guidAsString = (string)changeEventArgs.Value;

        if (Guid.TryParse(guidAsString, out var guidValue))
        {
            var themesInScope = themeState.ThemeRecordsList
                .Where(x =>
                    x.ThemeScopes.Contains(ThemeScope.App))
                .ToArray();

            var existingThemeRecord = themesInScope
                .FirstOrDefault(btr => btr.ThemeKey.Guid == guidValue);

            if (existingThemeRecord is not null)
                AppOptionsService.SetActiveThemeRecordKey(existingThemeRecord.ThemeKey);
        }
    }

    private bool CheckIsActiveValid(
        ThemeRecord[] themeRecords,
        ThemeKey activeThemeKey)
    {
        return themeRecords.Any(
            btr =>
                btr.ThemeKey == activeThemeKey);
    }

    private bool CheckIsActiveSelection(
        ThemeKey themeKey,
        ThemeKey activeThemeKey)
    {
        return themeKey == activeThemeKey;
    }

    private async void AppOptionsStateWrapOnStateChanged(object? sender, EventArgs e)
    {
        await InvokeAsync(StateHasChanged);
    }

    private async void ThemeStateWrapOnStateChanged(object? sender, EventArgs e)
    {
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        AppOptionsService.AppOptionsStateWrap.StateChanged -= AppOptionsStateWrapOnStateChanged;
        ThemeRecordsCollectionService.ThemeRecordsCollectionWrap.StateChanged -= ThemeStateWrapOnStateChanged;
    }
}