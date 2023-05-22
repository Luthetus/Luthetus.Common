using Luthetus.Common.RazorLib.Store.DropdownCase;
using Fluxor;
using Luthetus.Common.RazorLib;

namespace Luthetus.Common.RazorLib.Dropdown;

public interface IDropdownService : ILuthetusCommonService
{
    public IState<DropdownsState> DropdownsStateWrap { get; }

    public void AddActiveDropdownKey(DropdownKey dialogRecord);
    public void RemoveActiveDropdownKey(DropdownKey dropdownKey);
    public void ClearActiveDropdownKeysAction();
}