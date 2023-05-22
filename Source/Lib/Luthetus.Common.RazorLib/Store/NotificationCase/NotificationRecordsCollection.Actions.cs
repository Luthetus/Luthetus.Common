using Luthetus.Common.RazorLib.Notification;

namespace Luthetus.Common.RazorLib.Store.NotificationCase;

public partial class NotificationRecordsCollection
{
    public record RegisterAction(NotificationRecord NotificationRecord);
    public record DisposeAction(NotificationKey NotificationKey);
}