namespace Luthetus.Common.RazorLib.BackgroundTaskCase;

public interface IBackgroundTaskMonitor
{
    public IBackgroundTask? ExecutingBackgroundTask { get; }

    public event Action? ExecutingBackgroundTaskChanged;

    public void SetExecutingBackgroundTask(IBackgroundTask? backgroundTask);
}