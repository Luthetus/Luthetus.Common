namespace Luthetus.Common.RazorLib.Dialog;

public record DialogKey(Guid Guid)
{
    public static DialogKey NewDialogKey()
    {
        return new(Guid.NewGuid());
    }
}