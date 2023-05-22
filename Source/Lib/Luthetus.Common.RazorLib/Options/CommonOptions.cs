using Luthetus.Common.RazorLib.Theme;

namespace Luthetus.Common.RazorLib.Options;

public record CommonOptions(
    int? FontSizeInPixels,
    int? IconSizeInPixels,
    ThemeKey? ThemeKey,
    string? FontFamily);
