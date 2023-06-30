using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.StateHasChangedBoundaryCase;

public partial class StateHasChangedBoundary : ComponentBase
{
    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = null!;

    public async Task InvokeStateHasChangedAsync()
    {
        await InvokeAsync(StateHasChanged);
    }
}