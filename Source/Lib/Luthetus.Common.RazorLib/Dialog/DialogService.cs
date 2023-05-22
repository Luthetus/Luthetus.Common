using Luthetus.Common.RazorLib.Store.DialogCase;
using Fluxor;

namespace Luthetus.Common.RazorLib.Dialog;

public class DialogService : IDialogService
{
    private readonly IDispatcher _dispatcher;

    public DialogService(
        bool isEnabled,
        IDispatcher dispatcher,
        IState<DialogRecordsCollection> dialogRecordsCollectionWrap)
    {
        _dispatcher = dispatcher;
        IsEnabled = isEnabled;
        DialogRecordsCollectionWrap = dialogRecordsCollectionWrap;
    }

    public bool IsEnabled { get; }
    public IState<DialogRecordsCollection> DialogRecordsCollectionWrap { get; }

    public void RegisterDialogRecord(DialogRecord dialogRecord)
    {
        _dispatcher.Dispatch(
            new DialogRecordsCollection.RegisterAction(
                dialogRecord));
    }

    public void SetDialogRecordIsMaximized(DialogKey dialogKey, bool isMaximized)
    {
        _dispatcher.Dispatch(
            new DialogRecordsCollection.SetIsMaximizedAction(
                dialogKey,
                isMaximized));
    }

    public void DisposeDialogRecord(DialogKey dialogKey)
    {
        _dispatcher.Dispatch(
            new DialogRecordsCollection.DisposeAction(
                dialogKey));
    }
}