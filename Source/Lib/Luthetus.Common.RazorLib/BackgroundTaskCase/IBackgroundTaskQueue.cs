namespace Luthetus.Common.RazorLib.BackgroundTaskCase;

public interface IBackgroundTaskQueue
{
    public void QueueBackgroundWorkItem(
        IBackgroundTask backgroundTask);

    public Task<IBackgroundTask?> DequeueAsync(
        CancellationToken cancellationToken);
}