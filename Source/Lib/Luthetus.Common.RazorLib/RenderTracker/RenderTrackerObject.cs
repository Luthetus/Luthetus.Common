using System.Collections.Immutable;

namespace Luthetus.Common.RazorLib.RenderTracker;

public class RenderTrackerObject
{
    public RenderTrackerObject(
        string displayName,
        long onInitializedTicks,
        string onInitializedReason)
    {
        DisplayName = displayName;
        OnInitializedTicks = onInitializedTicks;
        OnInitializedReason = onInitializedReason;
        RenderTrackerEntries = ImmutableList<RenderTrackerEntry>.Empty;
    }

    public RenderTrackerObject(
        RenderTrackerObject cloneFrom,
        ImmutableList<RenderTrackerEntry> renderTrackerEntries)
    {
        DisplayName = cloneFrom.DisplayName;
        OnInitializedTicks = cloneFrom.OnInitializedTicks;
        OnInitializedReason = cloneFrom.OnInitializedReason;
        ShowEntries = cloneFrom.ShowEntries;
        RenderTrackerEntries = renderTrackerEntries;
    }

    public string DisplayName { get; } = null!;
    /// <summary>The DateTime in which the component was initialized, expressed as ticks.</summary>
    public long OnInitializedTicks { get; }
    /// <summary>Reason is useful for determining if the component never should have been rendered to begin with.</summary>
    public string OnInitializedReason { get; }
    public ImmutableList<RenderTrackerEntry> RenderTrackerEntries { get; }

    public bool ShowEntries { get; set; } = false;

    public int RenderCount => RenderTrackerEntries.Count;
}
