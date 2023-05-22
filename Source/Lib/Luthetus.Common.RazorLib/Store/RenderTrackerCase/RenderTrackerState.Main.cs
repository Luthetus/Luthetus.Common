using Fluxor;
using Luthetus.Common.RazorLib.RenderTracker;
using System.Collections.Immutable;

namespace Luthetus.Common.RazorLib.Store.RenderTrackerCase;

[FeatureState]
public partial class RenderTrackerState
{
    private RenderTrackerState()
    {
        Map = ImmutableDictionary<string, RenderTrackerObject>.Empty;
    }
    
    private RenderTrackerState(ImmutableDictionary<string, RenderTrackerObject> map)
    {
        Map = map;
    }

    public ImmutableDictionary<string, RenderTrackerObject> Map { get; }
}
