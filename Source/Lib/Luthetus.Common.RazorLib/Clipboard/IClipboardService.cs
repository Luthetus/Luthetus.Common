using Luthetus.Common.RazorLib;

namespace Luthetus.Common.RazorLib.Clipboard;

public interface IClipboardService : ILuthetusCommonService
{
    public Task<string> ReadClipboard();
    public Task SetClipboard(string value);
}