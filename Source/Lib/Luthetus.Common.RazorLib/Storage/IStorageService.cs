using Luthetus.Common.RazorLib;

namespace Luthetus.Common.RazorLib.Storage;

public interface IStorageService : ILuthetusCommonService
{
    public ValueTask SetValue(string key, object? value);
    public ValueTask<object?> GetValue(string key);
}