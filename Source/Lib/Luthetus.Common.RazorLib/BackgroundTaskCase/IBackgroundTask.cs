using Fluxor;

namespace Luthetus.Common.RazorLib.BackgroundTaskCase;

public interface IBackgroundTask
{
    public BackgroundTaskKey BackgroundTaskKey { get; }
    public string Name { get; }
    public string Description { get; }
    public bool ShouldNotifyWhenStartingWorkItem { get; }
    public Task? WorkProgress { get; }
    public Func<CancellationToken, Task> CancelFunc { get; }
    public IDispatcher? Dispatcher { get; }

    public Task InvokeWorkItem(CancellationToken cancellationToken);
}