using Luthetus.Common.RazorLib.BackgroundTaskCase.BaseTypes;
using Luthetus.Common.RazorLib.ComponentRenderers.Types;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.BackgroundTaskCase.Usage;

public partial class CommonBackgroundTaskDisplay : ComponentBase, IBackgroundTaskDisplayRendererType
{
    [Parameter, EditorRequired]
    public IBackgroundTask BackgroundTask { get; set; } = null!;
}