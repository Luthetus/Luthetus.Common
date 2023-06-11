﻿namespace Luthetus.Common.RazorLib.Reactive;

public interface IThrottle : IDisposable
{
    public static readonly TimeSpan DefaultThrottleTimeSpan = TimeSpan.FromMilliseconds(33);

    /// <summary>
    /// The cancellation logic should be made internal to the workItem Func itself.
    /// </summary>
    public Task FireAsync(
        Func<Task> workItem);
}
