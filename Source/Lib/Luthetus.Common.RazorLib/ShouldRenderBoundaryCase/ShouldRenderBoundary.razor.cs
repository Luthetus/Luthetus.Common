using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.ShouldRenderBoundaryCase;

public partial class ShouldRenderBoundary : ComponentBase
{
    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = null!;

    protected override bool ShouldRender() => false;
}