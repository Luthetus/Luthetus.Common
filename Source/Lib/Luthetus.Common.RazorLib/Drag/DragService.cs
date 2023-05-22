namespace Luthetus.Common.RazorLib.Drag;

public class DragService : IDragService
{
    public DragService(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }

    public bool IsEnabled { get; }
}