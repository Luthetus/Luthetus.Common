namespace Luthetus.Common.RazorLib.Reactive;

public class Throttle<TEventArgs> : IThrottle<TEventArgs>
{
    private readonly object _syncRoot = new();
    private readonly Stack<TEventArgs> _eventArgsStack = new();
    private readonly SemaphoreSlim _throttleSemaphoreSlim = new(1, 1);

    private CancellationTokenSource _throttleCancellationTokenSource = new();
    private Task _throttleDelayTask = Task.CompletedTask;

    public Throttle(TimeSpan throttleTimeSpan)
    {
        ThrottleTimeSpan = throttleTimeSpan;
    }

    public TimeSpan ThrottleTimeSpan { get; }

    public async Task<(TEventArgs? tEventArgs, bool isCancellationRequested)> FireAsync(
        TEventArgs eventArgs,
        CancellationToken externalCancellationToken)
    {
        CancellationToken throttleCancellationToken;

        lock (_syncRoot)
        {
            _eventArgsStack.Push(eventArgs);

            if (_eventArgsStack.Count > 1)
                return (default, true);

            throttleCancellationToken = _throttleCancellationTokenSource.Token;
        }

        await _throttleSemaphoreSlim.WaitAsync();

        try
        {
            await _throttleDelayTask;

            if (throttleCancellationToken.IsCancellationRequested ||
                externalCancellationToken.IsCancellationRequested)
            {
                return (default, true);
            }

            // WebAssembly Single Threaded
            await Task.Yield();

            lock (_syncRoot)
            {
                var mostRecentEventArgs = _eventArgsStack.Pop();
                _eventArgsStack.Clear();

                _throttleCancellationTokenSource.Cancel();
                _throttleCancellationTokenSource = new();

                _throttleDelayTask = Task.Run(async () =>
                {
                    await Task.Delay(
                        ThrottleTimeSpan,
                        externalCancellationToken);

                }, externalCancellationToken);

                return (mostRecentEventArgs, false);
            }
        }
        finally
        {
            _throttleSemaphoreSlim.Release();
        }
    }

    public void Dispose()
    {
        _throttleCancellationTokenSource.Cancel();
    }
}