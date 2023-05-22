namespace Luthetus.Common.RazorLib.RenderTracker;

public class RenderTrackerEntry
{
    public RenderTrackerEntry(
        long ticks,
        string reason)
    {
        Ticks = ticks;
        Reason = reason;
    }

    /// <summary>The DateTime in which the render was logged, expressed as ticks.</summary>
    public long Ticks { get; }
    /// <summary>Reason is useful for determining if redundant renders were occurring.</summary>
    public string Reason { get; } = null!;
}
