using Fluxor;

namespace Luthetus.Common.RazorLib.BackgroundTaskCase;

public class BackgroundTask : IBackgroundTask
{
    private readonly object _syncRoot = new();
    private readonly Func<CancellationToken, Task> _workItem;

    public BackgroundTask(
        Func<CancellationToken, Task> workItem,
        string name,
        string description,
        bool notifyWhenStartingWorkItem,
        Func<CancellationToken, Task> cancelFunc,
        IDispatcher? dispatcher,
        CancellationToken cancellationToken)
    {
        _workItem = workItem;
        Name = name;
        Description = description;
        CancelFunc = cancelFunc;
        Dispatcher = dispatcher;
        CancellationToken = cancellationToken;
        ShouldNotifyWhenStartingWorkItem = notifyWhenStartingWorkItem;
    }

    public BackgroundTask(
            Func<CancellationToken, Task> workItem,
            string name,
            string description,
            bool notifyWhenStartingWorkItem,
            Func<CancellationToken, Task> cancelFunc,
            IDispatcher? dispatcher,
            BackgroundTaskKey backgroundTaskKey,
            CancellationToken cancellationToken)
        : this(
            workItem,
            name,
            description,
            notifyWhenStartingWorkItem,
            cancelFunc,
            dispatcher,
            cancellationToken)
    {
        BackgroundTaskKey = backgroundTaskKey;
    }

    public BackgroundTaskKey BackgroundTaskKey { get; } = BackgroundTaskKey.NewBackgroundTaskKey();
    public string Name { get; }
    public string Description { get; }
    public bool ShouldNotifyWhenStartingWorkItem { get; }
    public Task? WorkProgress { get; private set; }
    public Func<CancellationToken, Task> CancelFunc { get; }
    /// <summary>
    /// The <see cref="QueuedHostedService"/> is a singleton, yet the IDispatcher is a
    /// scoped service. Therefore it must be passed in so the singleton can
    /// respond to the correct scope with a notification on the UI that the task is completed.
    /// </summary>
    public IDispatcher? Dispatcher { get; }
    public CancellationToken CancellationToken { get; }

    public Task InvokeWorkItem(CancellationToken cancellationToken)
    {
        lock (_syncRoot)
        {
            if (WorkProgress is null)
                WorkProgress = _workItem.Invoke(cancellationToken);

            return WorkProgress;
        }
    }
}