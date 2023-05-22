using Luthetus.Common.RazorLib.Store.DropdownCase;
using Fluxor;
using Luthetus.Common.RazorLib.Dropdown;
using Luthetus.Common.RazorLib.Keyboard;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Luthetus.Common.RazorLib.Menu;

public partial class MenuOptionDisplay : ComponentBase
{
    [Inject]
    private IState<DropdownsState> DropdownStatesWrap { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public MenuOptionRecord MenuOptionRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public int Index { get; set; }
    [Parameter, EditorRequired]
    public int ActiveMenuOptionRecordIndex { get; set; }
    [Parameter]
    public RenderFragment<MenuOptionRecord>? IconRenderFragment { get; set; }

    private readonly DropdownKey _subMenuDropdownKey = DropdownKey.NewDropdownKey();
    private ElementReference? _menuOptionDisplayElementReference;
    private bool _shouldDisplayWidget;

    private bool IsActive => Index == ActiveMenuOptionRecordIndex;
    private bool HasSubmenuActive => DropdownStatesWrap.Value.ActiveDropdownKeys
        .Any(x => x.Guid == _subMenuDropdownKey.Guid);

    private string IsActiveCssClass => IsActive
        ? "bcrl_active"
        : string.Empty;

    private string HasWidgetActiveCssClass => _shouldDisplayWidget &&
                                              MenuOptionRecord.WidgetRendererType is not null
        ? "bcrl_active"
        : string.Empty;

    private MenuOptionWidgetParameters MenuOptionWidgetParameters => new(
        () => HideWidgetAsync(null),
        HideWidgetAsync);

    private bool DisplayWidget => _shouldDisplayWidget &&
                                  MenuOptionRecord.WidgetRendererType is not null;

    protected override async Task OnParametersSetAsync()
    {
        var localHasSubmenuActive = HasSubmenuActive;

        // The following if(s) are not working. They result
        // in one never being able to open a submenu
        //
        // // Close submenu for non active menu option
        // if (!IsActive && localHasSubmenuActive)
        //     Dispatcher.Dispatch(new RemoveActiveDropdownKeyAction(_subMenuDropdownKey));
        //
        // // Hide widget for non active menu option
        // if (!IsActive && _shouldDisplayWidget)
        // {
        //     _shouldDisplayWidget = false;
        // }

        // Set focus to active menu option
        if (IsActive &&
            !localHasSubmenuActive &&
            !DisplayWidget &&
            _menuOptionDisplayElementReference.HasValue)
        {
            try
            {
                await _menuOptionDisplayElementReference.Value.FocusAsync();
            }
            catch (Exception e)
            {
                // 2023-04-18: The app has had a bug where it "freezes" and must be restarted.
                //             This bug is seemingly happening randomly. I have a suspicion
                //             that there are race-condition exceptions occurring with "FocusAsync"
                //             on an ElementReference.
            }
        }

        await base.OnParametersSetAsync();
    }

    private void HandleOnClick()
    {
        if (MenuOptionRecord.OnClick is not null)
        {
            MenuOptionRecord.OnClick.Invoke();
            Dispatcher.Dispatch(new DropdownsState.ClearActivesAction());
        }

        if (MenuOptionRecord.SubMenu is not null)
        {
            if (HasSubmenuActive)
                Dispatcher.Dispatch(new DropdownsState.RemoveActiveAction(_subMenuDropdownKey));
            else
                Dispatcher.Dispatch(new DropdownsState.AddActiveAction(_subMenuDropdownKey));
        }

        if (MenuOptionRecord.WidgetRendererType is not null)
        {
            _shouldDisplayWidget = !_shouldDisplayWidget;
        }
    }

    private void HandleOnKeyDown(KeyboardEventArgs keyboardEventArgs)
    {
        switch (keyboardEventArgs.Key)
        {
            case KeyboardKeyFacts.MovementKeys.ARROW_RIGHT:
            case KeyboardKeyFacts.AlternateMovementKeys.ARROW_RIGHT:
                if (MenuOptionRecord.SubMenu is not null)
                    Dispatcher.Dispatch(new DropdownsState.AddActiveAction(_subMenuDropdownKey));
                break;
        }

        switch (keyboardEventArgs.Code)
        {
            case KeyboardKeyFacts.WhitespaceCodes.ENTER_CODE:
            case KeyboardKeyFacts.WhitespaceCodes.SPACE_CODE:
                HandleOnClick();
                break;
        }
    }

    private async Task HideWidgetAsync(Action? onAfterWidgetHidden)
    {
        _shouldDisplayWidget = false;
        await InvokeAsync(StateHasChanged);

        if (onAfterWidgetHidden is null)
        {
            try
            {
                if (_menuOptionDisplayElementReference.HasValue)
                    await _menuOptionDisplayElementReference.Value.FocusAsync();
            }
            catch (Exception e)
            {
                // 2023-04-18: The app has had a bug where it "freezes" and must be restarted.
                //             This bug is seemingly happening randomly. I have a suspicion
                //             that there are race-condition exceptions occurring with "FocusAsync"
                //             on an ElementReference.
            }
        }
        else
        {
            onAfterWidgetHidden.Invoke();
            Dispatcher.Dispatch(new DropdownsState.ClearActivesAction());
        }
    }
}