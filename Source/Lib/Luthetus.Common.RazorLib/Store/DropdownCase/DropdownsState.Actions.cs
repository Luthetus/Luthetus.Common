using Luthetus.Common.RazorLib.Dropdown;

namespace Luthetus.Common.RazorLib.Store.DropdownCase;

public partial record DropdownsState
{
    public record AddActiveAction(DropdownKey DropdownKey);
    public record RemoveActiveAction(DropdownKey DropdownKey);
    public record ClearActivesAction;
}