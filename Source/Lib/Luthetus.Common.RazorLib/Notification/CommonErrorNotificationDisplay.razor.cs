using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.Notification;

public partial class CommonErrorNotificationDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public string Message { get; set; } = null!;
}