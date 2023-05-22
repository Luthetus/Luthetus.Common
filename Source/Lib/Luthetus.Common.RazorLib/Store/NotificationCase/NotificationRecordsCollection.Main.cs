using System.Collections.Immutable;
using Fluxor;
using Luthetus.Common.RazorLib.Notification;

namespace Luthetus.Common.RazorLib.Store.NotificationCase;

/// <summary>
/// Keep the <see cref="NotificationRecordsCollection"/> as a class
/// as to avoid record value comparisons when Fluxor checks
/// if the <see cref="FeatureStateAttribute"/> has been replaced.
/// </summary>
[FeatureState]
public partial class NotificationRecordsCollection
{
    public NotificationRecordsCollection()
    {
        Notifications = ImmutableList<NotificationRecord>.Empty;
    }
    
    public ImmutableList<NotificationRecord> Notifications { get; init; }
}