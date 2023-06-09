namespace Luthetus.Common.RazorLib.BackgroundTaskCase.BaseTypes;

public record BackgroundTaskKey(Guid Guid)
{
    public static readonly BackgroundTaskKey Empty = new(Guid.Empty);

    public static BackgroundTaskKey NewBackgroundTaskKey()
    {
        return new(Guid.NewGuid());
    }
}