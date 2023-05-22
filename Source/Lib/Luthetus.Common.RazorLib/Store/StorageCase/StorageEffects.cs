using Fluxor;
using Luthetus.Common.RazorLib.Storage;

namespace Luthetus.Common.RazorLib.Store.StorageCase;

public class StorageEffects
{
    private readonly IStorageService _storageService;

    public StorageEffects(
        IStorageService storageService)
    {
        _storageService = storageService;
    }

    public record WriteToStorageAction(string Key, object Value);

    [EffectMethod]
    public async Task HandleWriteGlobalTextEditorOptionsToLocalStorageAction(
        WriteToStorageAction writeToStorageAction,
        IDispatcher dispatcher)
    {
        var valueJson = System.Text.Json.JsonSerializer
            .Serialize(writeToStorageAction.Value);

        await _storageService.SetValue(
            writeToStorageAction.Key,
            valueJson);
    }
}