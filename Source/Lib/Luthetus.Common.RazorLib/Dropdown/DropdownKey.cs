namespace Luthetus.Common.RazorLib.Dropdown;

/// <summary>
/// <see cref="DropdownKey"/> is the only Type associated with
/// a "dropdown".
/// <br/><br/>
/// The <see cref="DropdownKey"/> used to show, or not to show,
/// the given Blazor component with a typically non-static position for css.
/// </summary>
/// <param name="Guid"></param>
public record DropdownKey(Guid Guid)
{
    public static DropdownKey NewDropdownKey()
    {
        return new(Guid.NewGuid());
    }
}