using Luthetus.Common.RazorLib.Store.NotificationCase;
using Fluxor;
using Luthetus.Common.RazorLib;

namespace Luthetus.Common.RazorLib.Notification;

public interface INotificationService : ILuthetusCommonService
{
    public IState<NotificationRecordsCollection> NotificationRecordsCollectionWrap { get; }

    public void RegisterNotificationRecord(NotificationRecord notificationRecord);
    public void DisposeNotificationRecord(NotificationKey dialogKey);
}