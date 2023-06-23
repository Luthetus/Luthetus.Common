using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Luthetus.Common.RazorLib.CustomEvents;
using Luthetus.Common.RazorLib.Icons;
using Luthetus.Common.RazorLib.Icons.Codicon;

namespace Luthetus.Common.RazorLib.EncapsulateEventStateHasChangedCase;

/// <summary>
/// Events (such as @onclick) internally invoke 'StateHasChanged'.
/// Perhaps by using parameter splatting
/// https://learn.microsoft.com/en-us/aspnet/core/blazor/components/splat-attributes-and-arbitrary-parameters?view=aspnetcore-7.0
/// one can wrap the event as its own blazor component so the 'StateHasChanged'
/// becomes isolated to a smaller scale.
/// </summary>
public partial class EncapsulateEventStateHasChanged : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; } = null!;
}