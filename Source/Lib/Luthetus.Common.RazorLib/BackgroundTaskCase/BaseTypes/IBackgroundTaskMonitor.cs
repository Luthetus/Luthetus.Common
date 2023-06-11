namespace Luthetus.Common.RazorLib.BackgroundTaskCase.BaseTypes;

public interface IBackgroundTaskMonitor
{
    public IBackgroundTask? ExecutingBackgroundTask { get; }

    public event Action? ExecutingBackgroundTaskChanged;

    public void SetExecutingBackgroundTask(IBackgroundTask? backgroundTask);
}
