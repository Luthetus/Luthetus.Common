namespace Luthetus.Common.RazorLib.Notification;

/// <summary>
/// If <see cref="NotificationOverlayLifespan"/> is null
/// then it will not be removed by the default timer based auto removal task.
/// </summary>
public record NotificationRecord(
    NotificationKey NotificationKey,
    string Title,
    Type RendererType,
    Dictionary<string, object?>? Parameters,
    TimeSpan? NotificationOverlayLifespan,
    string? CssClassString);