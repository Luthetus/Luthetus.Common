using System.Collections.Immutable;

namespace Luthetus.Common.RazorLib.Menu;

public record MenuRecord(ImmutableArray<MenuOptionRecord> MenuOptions)
{
    public static readonly MenuRecord Empty = new(
        new[]
        {
            new MenuOptionRecord(
                "No menu options exist for this item.",
                MenuOptionKind.Other)
        }.ToImmutableArray());
}