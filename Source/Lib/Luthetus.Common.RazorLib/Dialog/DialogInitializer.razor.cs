using Luthetus.Common.RazorLib.Store.DialogCase;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.Dialog;

public partial class DialogInitializer : FluxorComponent
{
    [Inject]
    private IState<DialogRecordsCollection> DialogRecordsCollectionWrap { get; set; } = null!;
}