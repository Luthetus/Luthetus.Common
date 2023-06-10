using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Luthetus.Common.RazorLib.CustomEvents;
using Luthetus.Common.RazorLib.Icons;
using Luthetus.Common.RazorLib.Icons.Codicon;

namespace Luthetus.Common.RazorLib.StateHasChangedBoundaryCase;

public partial class StateHasChangedBoundary : ComponentBase
{
    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = null!;

    /// <summary>
    /// I had planned to use a Blazor Component '@ref="fieldName"' to get a reference.
    /// And then since the Blazor Component rendering this would be ComponentBase
    /// they would be able to invoke the protected method "StateHasChanged" directly
    /// due to sharing the same class. I think I'm being a goof and should
    /// re-read what protected means because that isn't working lol
    /// <br/><br/>
    /// Adding this method however, it works as I expected.
    /// </summary>
    public async Task InvokeStateHasChangedAsync()
    {
        await InvokeAsync(StateHasChanged);
    }
}