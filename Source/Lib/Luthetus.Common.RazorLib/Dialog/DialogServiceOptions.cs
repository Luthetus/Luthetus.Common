namespace Luthetus.Common.RazorLib.Dialog;

public record DialogServiceOptions
{
    public string IsMaximizedStyleCssString { get; init; } =
        "width: 100vw; height: 100vh; left: 0; top: 0;";
}