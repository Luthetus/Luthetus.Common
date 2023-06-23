using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.RenderTracker;

/// <example>
/// Render this component with an @ref="_fieldName".
/// In override OnAfterRender invoke _fieldName.IncrementCount();
/// This will re-render the child component with the exact amount of 'OnAfterRender'
/// invocations that occurred without an infinite loop / an eager counter that starts at 1.
/// </example>
public partial class RenderCounterDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public string DisplayName { get; set; } = null!;
    /// <summary>
    /// When set to true <see cref="ShouldAutoIncrement"/> will eagerly increment the
    /// <see cref="_renderCount"/> prior to a render having occurred.
    /// This way on the user interface the render counter reflects the true render count.
    /// Without this eager incrementation an infinite loop would occur if one incremented
    /// from within <see cref="OnAfterRenderAsync(bool)"/> then tried to 'InvokeAsync(StateHasChanged)
    /// to display the updated render count.
    /// <br/><br/>
    /// When set to false <see cref="ShouldAutoIncrement"/> the parent component is expected
    /// to have logic to increment the <see cref="_renderCount"/> from within that parent component's
    /// lifecycle logic. Then the parent component invokes this <see cref="RenderCounterDisplay"/>'s StateHasChanged method
    /// so this component is rendered without an infinite loop. All this incrementation logic is encapsulated within
    /// <see cref="IncrementCountAsync"/> and <see cref="IncrementCount"/>
    /// </summary>
    [Parameter]
    public bool ShouldAutoIncrement { get; set; }
    /// <summary>
    /// I want to use a conditional breakpoint while debugging. I'm using this component a lot
    /// and need to find the one I'm interested in.
    /// </summary>
    [Parameter]
    public Guid Id { get; set; } = Guid.Empty;

    private int _renderCount;

    private static readonly ApplicationException ShouldAutoIncrementApplicationException = 
        new ApplicationException($"Do not mix usage of the {nameof(ShouldAutoIncrement)} parameter being set to true, and manual invocation of either: {nameof(IncrementCount)}, or {nameof(IncrementCountAsync)}");

    protected override bool ShouldRender()
    {
        var shouldRender = base.ShouldRender();

        return shouldRender;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (ShouldAutoIncrement)
            {
                // Begin eager incrementation by having this first render count as 2 renders.
                _renderCount += 2;
                await InvokeAsync(StateHasChanged);
            }    
        }
        else
        {
            if (ShouldAutoIncrement)
                _renderCount++;
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    /// <summary>
    /// If guaranteed to be on the UI thread one might be interested in <see cref="IncrementCount"/> instead.
    /// </summary>
    public async Task IncrementCountAsync()
    {
        await InvokeAsync(IncrementCount);
    }
    
    /// <summary>
    /// If one is not guaranteed to be on the UI thread one might be interested in <see cref="IncrementCountAsync"/> instead.
    /// </summary>
    public void IncrementCount()
    {
        if (ShouldAutoIncrement)
            throw ShouldAutoIncrementApplicationException;

        _renderCount++;

        StateHasChanged();
    }
}