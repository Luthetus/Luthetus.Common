using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Luthetus.Common.RazorLib.CustomEvents;
using Luthetus.Common.RazorLib.Icons;
using Luthetus.Common.RazorLib.Icons.Codicon;

namespace Luthetus.Common.RazorLib.ShouldRenderBoundaryCase;

public partial class ShouldRenderBoundary : ComponentBase
{
    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = null!;

    protected override bool ShouldRender() => false;
}