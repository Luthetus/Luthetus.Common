using Luthetus.Common.RazorLib.Store.DialogCase;
using Fluxor;

namespace Luthetus.Common.RazorLib.Dialog;

public interface IDialogService : ILuthetusCommonService
{
    public IState<DialogRecordsCollection> DialogRecordsCollectionWrap { get; }

    public void RegisterDialogRecord(DialogRecord dialogRecord);
    public void SetDialogRecordIsMaximized(DialogKey dialogKey, bool isMaximized);
    public void DisposeDialogRecord(DialogKey dialogKey);
}