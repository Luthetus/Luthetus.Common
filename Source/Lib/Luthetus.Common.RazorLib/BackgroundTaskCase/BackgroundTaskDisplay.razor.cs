using Luthetus.Common.RazorLib.ComponentRenderers.Types;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.BackgroundTaskCase;

public partial class BackgroundTaskDisplay : ComponentBase, IBackgroundTaskDisplayRendererType
{
    [Parameter, EditorRequired]
    public IBackgroundTask BackgroundTask { get; set; } = null!;
}