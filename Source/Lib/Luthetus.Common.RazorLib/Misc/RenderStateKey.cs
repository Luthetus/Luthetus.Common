namespace Luthetus.Common.RazorLib.Misc;

/// <summary>Used to re-render Blazor Components. If a <see cref="RenderStateKey"/> value changes, then the given Blazor Component should re-render.</summary>
public record RenderStateKey(Guid Guid)
{
    public static readonly RenderStateKey Empty = new RenderStateKey(Guid.Empty);

    public static RenderStateKey NewRenderStateKey()
    {
        return new RenderStateKey(Guid.NewGuid());
    }
}