﻿using Luthetus.Common.RazorLib.BackgroundTaskCase.BaseTypes;
using Luthetus.Common.RazorLib.BackgroundTaskCase.Usage;

namespace Luthetus.Common.RazorLib.BackgroundTaskCase.Usage;

public class CommonBackgroundTaskQueueSingleThreaded : ICommonBackgroundTaskQueue
{
    public void QueueBackgroundWorkItem(
        IBackgroundTask backgroundTask)
    {
        backgroundTask
            .InvokeWorkItem(CancellationToken.None)
            .Wait();
    }

    public Task<IBackgroundTask?> DequeueAsync(
        CancellationToken cancellationToken)
    {
        return Task.FromResult(default(IBackgroundTask?));
    }
}