namespace Luthetus.Common.RazorLib.Documentation;

public class LuthetusDocumentAttribute : Attribute
{
    public LuthetusDocumentAttribute(string displayName)
    {
        DisplayName = displayName;
    }

    public string DisplayName { get; }
}
