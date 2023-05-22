namespace Luthetus.Common.RazorLib.Reactive;

public interface IThrottle<TEventArgs> : IDisposable
{
    public Task<(TEventArgs? tEventArgs, bool isCancellationRequested)> FireAsync(
        TEventArgs eventArgs,
        CancellationToken externalCancellationToken);
}