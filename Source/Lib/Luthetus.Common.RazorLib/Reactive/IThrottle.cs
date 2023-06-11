﻿namespace Luthetus.Common.RazorLib.Reactive;

public interface IThrottle : IDisposable
{
    /// <summary>
    /// The cancellation logic should be made internal to the workItem Func itself.
    /// </summary>
    public Task FireAsync(
        Func<Task> workItem);
}
