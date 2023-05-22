using Luthetus.Common.RazorLib.Dialog;

namespace Luthetus.Common.RazorLib.Store.DialogCase;

public partial class DialogRecordsCollection
{
    public record RegisterAction(DialogRecord DialogRecord);
    public record DisposeAction(DialogKey DialogKey);
    public record SetIsMaximizedAction(DialogKey DialogKey, bool IsMaximized);
}