using Fluxor;

namespace Luthetus.Common.RazorLib.Store.NotificationCase;

public partial class NotificationRecordsCollection
{
    private class Reducer
    {
        [ReducerMethod]
        public static NotificationRecordsCollection ReduceRegisterAction(
            NotificationRecordsCollection inRecordsCollection,
            RegisterAction registerAction)
        {
            var nextList = inRecordsCollection.Notifications
                .Add(registerAction.NotificationRecord);

            return new NotificationRecordsCollection
            {
                Notifications = nextList
            };
        }
        
        [ReducerMethod]
        public static NotificationRecordsCollection ReduceDisposeAction(
            NotificationRecordsCollection inRecordsCollection,
            DisposeAction disposeAction)
        {
            var notification = inRecordsCollection.Notifications
                .FirstOrDefault(x => 
                    x.NotificationKey == disposeAction.NotificationKey);

            if (notification is null)
                return inRecordsCollection;
            
            var nextList = inRecordsCollection.Notifications
                .Remove(notification);

            return new NotificationRecordsCollection
            {
                Notifications = nextList
            };
        }
    }
}