using Luthetus.Common.RazorLib.Theme;

namespace Luthetus.Common.RazorLib.Store.ThemeCase;

public partial class ThemeRecordsCollection
{
    public record RegisterAction(ThemeRecord ThemeRecord);
    public record DisposeAction(ThemeKey ThemeKey);
}