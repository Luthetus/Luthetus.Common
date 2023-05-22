using System.Collections.Immutable;
using Fluxor;
using Luthetus.Common.RazorLib.Dialog;

namespace Luthetus.Common.RazorLib.Store.DialogCase;

/// <summary>
/// Keep the <see cref="DialogRecordsCollection"/> as a class
/// as to avoid record value comparisons when Fluxor checks
/// if the <see cref="FeatureStateAttribute"/> has been replaced.
/// </summary>
[FeatureState]
public partial class DialogRecordsCollection
{
    public DialogRecordsCollection()
    {
        DialogRecords = ImmutableList<DialogRecord>.Empty;
    }
    
    public ImmutableList<DialogRecord> DialogRecords { get; init; } 
}