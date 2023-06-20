using Luthetus.Common.RazorLib.BackgroundTaskCase.BaseTypes;

namespace Luthetus.Common.RazorLib.BackgroundTaskCase.Usage;

public class CommonBackgroundTaskQueueSingleThreaded : ICommonBackgroundTaskQueue
{
    public void QueueBackgroundWorkItem(
        IBackgroundTask backgroundTask)
    {
        _ = Task.Run(async () =>
        {
            await backgroundTask
                .InvokeWorkItem(CancellationToken.None);
        });
    }

    public Task<IBackgroundTask?> DequeueAsync(
        CancellationToken cancellationToken)
    {
        return Task.FromResult(default(IBackgroundTask?));
    }
}