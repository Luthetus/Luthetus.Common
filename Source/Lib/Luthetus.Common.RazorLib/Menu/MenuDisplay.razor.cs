using Luthetus.Common.RazorLib.Store.DropdownCase;
using Fluxor;
using Luthetus.Common.RazorLib.Dropdown;
using Luthetus.Common.RazorLib.Keyboard;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Luthetus.Common.RazorLib.Menu;

public partial class MenuDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [CascadingParameter(Name = "ReturnFocusToParentFuncAsync")]
    public Func<Task>? ReturnFocusToParentFuncAsync { get; set; }
    [CascadingParameter]
    public DropdownKey? DropdownKey { get; set; }

    [Parameter, EditorRequired]
    public MenuRecord MenuRecord { get; set; } = null!;
    [Parameter]
    public int InitialActiveMenuOptionRecordIndex { get; set; } = -1;
    [Parameter]
    public bool GroupByMenuOptionKind { get; set; } = true;
    [Parameter]
    public bool FocusOnAfterRenderAsync { get; set; } = true;
    [Parameter]
    public RenderFragment<MenuOptionRecord>? IconRenderFragment { get; set; }

    private ElementReference? _menuDisplayElementReference;

    /// <summary>
    /// First time the MenuDisplay opens
    /// the _activeMenuOptionRecordIndex == -1
    /// </summary>
    private int _activeMenuOptionRecordIndex = -1;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _activeMenuOptionRecordIndex = InitialActiveMenuOptionRecordIndex;

            if (FocusOnAfterRenderAsync &&
                _activeMenuOptionRecordIndex == -1 &&
                _menuDisplayElementReference is not null)
            {
                try
                {
                    await _menuDisplayElementReference.Value.FocusAsync();
                }
                catch (Exception)
                {
                    // 2023-04-18: The app has had a bug where it "freezes" and must be restarted.
                    //             This bug is seemingly happening randomly. I have a suspicion
                    //             that there are race-condition exceptions occurring with "FocusAsync"
                    //             on an ElementReference.
                }
            }
            else
            {
                await InvokeAsync(StateHasChanged);
            }
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    public async Task SetFocusToFirstOptionInMenuAsync()
    {
        _activeMenuOptionRecordIndex = 0;

        await InvokeAsync(StateHasChanged);
    }

    private async Task RestoreFocusToThisMenuAsync()
    {
        if (_activeMenuOptionRecordIndex == -1)
        {
            try
            {
                if (_menuDisplayElementReference is not null)
                    await _menuDisplayElementReference.Value.FocusAsync();
            }
            catch (Exception)
            {
                // 2023-04-18: The app has had a bug where it "freezes" and must be restarted.
                //             This bug is seemingly happening randomly. I have a suspicion
                //             that there are race-condition exceptions occurring with "FocusAsync"
                //             on an ElementReference.
            }

            await InvokeAsync(StateHasChanged);
        }
    }

    private void HandleOnKeyDown(KeyboardEventArgs keyboardEventArgs)
    {
        if (MenuRecord.MenuOptions.Length == 0)
        {
            _activeMenuOptionRecordIndex = -1;
            return;
        }

        switch (keyboardEventArgs.Key)
        {
            case KeyboardKeyFacts.MovementKeys.ARROW_LEFT:
            case KeyboardKeyFacts.AlternateMovementKeys.ARROW_LEFT:
                if (DropdownKey is not null &&
                    ReturnFocusToParentFuncAsync is not null)
                {
                    Dispatcher.Dispatch(new DropdownsState.RemoveActiveAction(DropdownKey));
                    ReturnFocusToParentFuncAsync.Invoke();
                }
                break;
            case KeyboardKeyFacts.MovementKeys.ARROW_DOWN:
            case KeyboardKeyFacts.AlternateMovementKeys.ARROW_DOWN:
                if (_activeMenuOptionRecordIndex >= MenuRecord.MenuOptions.Length - 1)
                    _activeMenuOptionRecordIndex = 0;
                else
                    _activeMenuOptionRecordIndex++;
                break;
            case KeyboardKeyFacts.MovementKeys.ARROW_UP:
            case KeyboardKeyFacts.AlternateMovementKeys.ARROW_UP:
                if (_activeMenuOptionRecordIndex <= 0)
                    _activeMenuOptionRecordIndex = MenuRecord.MenuOptions.Length - 1;
                else
                    _activeMenuOptionRecordIndex--;
                break;
            case KeyboardKeyFacts.MovementKeys.HOME:
                _activeMenuOptionRecordIndex = 0;
                break;
            case KeyboardKeyFacts.MovementKeys.END:
                _activeMenuOptionRecordIndex = MenuRecord.MenuOptions.Length - 1;
                break;
            case KeyboardKeyFacts.MetaKeys.ESCAPE:
                if (DropdownKey is not null)
                    Dispatcher.Dispatch(new DropdownsState.RemoveActiveAction(DropdownKey));

                if (ReturnFocusToParentFuncAsync is not null)
                    ReturnFocusToParentFuncAsync.Invoke();

                break;
        }
    }
}