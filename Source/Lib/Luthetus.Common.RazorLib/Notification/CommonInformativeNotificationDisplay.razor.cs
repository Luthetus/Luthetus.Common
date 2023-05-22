using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.Notification;

public partial class CommonInformativeNotificationDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public string Message { get; set; } = null!;
}