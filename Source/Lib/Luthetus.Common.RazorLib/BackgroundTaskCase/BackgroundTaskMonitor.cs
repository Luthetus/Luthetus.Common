using Luthetus.Common.RazorLib.Store.NotificationCase;
using Luthetus.Common.RazorLib.ComponentRenderers;
using Luthetus.Common.RazorLib.ComponentRenderers.Types;
using Luthetus.Common.RazorLib.Notification;

namespace Luthetus.Common.RazorLib.BackgroundTaskCase;

public class BackgroundTaskMonitor : IBackgroundTaskMonitor
{
    private readonly ILuthetusCommonComponentRenderers _luthetusCommonComponentRenderers;

    public BackgroundTaskMonitor(
        ILuthetusCommonComponentRenderers luthetusCommonComponentRenderers)
    {
        _luthetusCommonComponentRenderers = luthetusCommonComponentRenderers;
    }

    public IBackgroundTask? ExecutingBackgroundTask { get; private set; }

    public event Action? ExecutingBackgroundTaskChanged;

    public void SetExecutingBackgroundTask(IBackgroundTask? backgroundTask)
    {
        ExecutingBackgroundTask = backgroundTask;
        ExecutingBackgroundTaskChanged?.Invoke();

        if (backgroundTask is not null &&
            backgroundTask.ShouldNotifyWhenStartingWorkItem &&
            backgroundTask.Dispatcher is not null &&
            _luthetusCommonComponentRenderers.BackgroundTaskDisplayRendererType is not null)
        {
            var notificationRecord = new NotificationRecord(
                NotificationKey.NewNotificationKey(),
                $"Starting: {backgroundTask.Name}",
                _luthetusCommonComponentRenderers.BackgroundTaskDisplayRendererType,
                new Dictionary<string, object?>
                {
                    {
                        nameof(IBackgroundTaskDisplayRendererType.BackgroundTask),
                        backgroundTask
                    }
                },
                null,
                null);

            backgroundTask.Dispatcher.Dispatch(
                new NotificationRecordsCollection.RegisterAction(
                    notificationRecord));
        }
    }
}