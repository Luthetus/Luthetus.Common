using Luthetus.Common.RazorLib.Store.RenderTrackerCase;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.RenderTracker;

public partial class RenderTrackerDisplay : FluxorComponent
{
    [Inject]
    public IState<RenderTrackerState> RenderTrackerStateWrap { get; set; } = null!;

    private int _numberOfEntriesToShow = 30;
}