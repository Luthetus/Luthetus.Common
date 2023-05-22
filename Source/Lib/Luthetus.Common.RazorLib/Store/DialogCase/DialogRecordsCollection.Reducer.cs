using Fluxor;

namespace Luthetus.Common.RazorLib.Store.DialogCase;

public partial class DialogRecordsCollection
{
    private class Reducer
    {
        [ReducerMethod]
        public static DialogRecordsCollection ReduceRegisterAction(
            DialogRecordsCollection inRecordsCollection,
            RegisterAction registerAction)
        {
            if (inRecordsCollection.DialogRecords
                .Any(x =>
                    x.DialogKey == registerAction.DialogRecord.DialogKey))
            {
                return inRecordsCollection;
            }
        
            var nextList = inRecordsCollection.DialogRecords
                .Add(registerAction.DialogRecord);

            return new DialogRecordsCollection
            {
                DialogRecords = nextList
            };
        }
        
        [ReducerMethod]
        public static DialogRecordsCollection ReduceSetIsMaximizedAction(
            DialogRecordsCollection inRecordsCollection,
            SetIsMaximizedAction setIsMaximizedAction)
        {
            var dialog = inRecordsCollection.DialogRecords
                .FirstOrDefault(x =>
                    x.DialogKey == setIsMaximizedAction.DialogKey);
        
            if (dialog is null)
                return inRecordsCollection;
        
            var nextList = inRecordsCollection.DialogRecords
                .Replace(
                    dialog, 
                    dialog with
                    {
                        IsMaximized = setIsMaximizedAction.IsMaximized
                    });

            return new DialogRecordsCollection
            {
                DialogRecords = nextList
            };
        }
    
        [ReducerMethod]
        public static DialogRecordsCollection ReduceDisposeAction(
            DialogRecordsCollection inRecordsCollection,
            DisposeAction disposeAction)
        {
            var existingDialog = inRecordsCollection.DialogRecords
                .FirstOrDefault(x =>
                    x.DialogKey == disposeAction.DialogKey);

            if (existingDialog is null)
                return inRecordsCollection;
            
            var nextList = inRecordsCollection.DialogRecords
                .Remove(existingDialog);

            return new DialogRecordsCollection
            {
                DialogRecords = nextList
            };
        }
    }
}