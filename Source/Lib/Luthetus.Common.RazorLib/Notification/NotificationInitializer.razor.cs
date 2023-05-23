using Luthetus.Common.RazorLib.Store.NotificationCase;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Luthetus.Common.RazorLib.Dropdown;
using Luthetus.Common.RazorLib.Store.DropdownCase;

namespace Luthetus.Common.RazorLib.Notification;

public partial class NotificationInitializer : FluxorComponent
{
    [Inject]
    private IState<NotificationRecordsCollection> NotificationStateWrap { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;
    
    private bool _disposed;

    protected override void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _disposed = true;

            var notificationState = NotificationStateWrap.Value;

            foreach (var notification in notificationState.Notifications)
            {
                Dispatcher.Dispatch(
                    new NotificationRecordsCollection.DisposeAction(
                        notification.NotificationKey));
            }
        }

        base.Dispose(disposing);
    }
}