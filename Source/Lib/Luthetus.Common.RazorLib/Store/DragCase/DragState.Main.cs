using Fluxor;
using Microsoft.AspNetCore.Components.Web;

namespace Luthetus.Common.RazorLib.Store.DragCase;

/// <summary>
/// Keep the <see cref="DragState"/> as a class
/// as to avoid record value comparisons when Fluxor checks
/// if the <see cref="FeatureStateAttribute"/> has been replaced.
/// </summary>
[FeatureState]
public partial class DragState
{
    public DragState()
    {
        ShouldDisplay = false;

        MouseEventArgs = null;
    }
    
    public bool ShouldDisplay { get; init; }
    public MouseEventArgs? MouseEventArgs { get; init; }
}