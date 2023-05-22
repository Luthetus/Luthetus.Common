using Fluxor;

namespace Luthetus.Common.RazorLib.Store.ThemeCase;

public partial class ThemeRecordsCollection
{
    private class Reducer
    {
        [ReducerMethod]
        public ThemeRecordsCollection ReduceRegisterAction(
            ThemeRecordsCollection inThemeRecordsCollection,
            RegisterAction registerAction)
        {
            var existingThemeRecord = inThemeRecordsCollection.ThemeRecordsList
                .FirstOrDefault(btr => 
                    btr.ThemeKey == registerAction.ThemeRecord.ThemeKey);

            if (existingThemeRecord is not null)
                return inThemeRecordsCollection;

            var nextList = inThemeRecordsCollection.ThemeRecordsList
                .Add(registerAction.ThemeRecord);
            
            return new ThemeRecordsCollection
            {
                ThemeRecordsList = nextList
            };
        }
        
        [ReducerMethod]
        public ThemeRecordsCollection ReduceDisposeAction(
            ThemeRecordsCollection inThemeRecordsCollection,
            DisposeAction disposeAction)
        {
            var existingThemeRecord = inThemeRecordsCollection.ThemeRecordsList
                .FirstOrDefault(btr => 
                    btr.ThemeKey == disposeAction.ThemeKey);

            if (existingThemeRecord is null)
                return inThemeRecordsCollection;

            var nextList = inThemeRecordsCollection.ThemeRecordsList
                .Remove(existingThemeRecord);
            
            return new ThemeRecordsCollection
            {
                ThemeRecordsList = nextList
            };
        }
    }
}