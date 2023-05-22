namespace Luthetus.Common.RazorLib.TreeView.Installation;

public class ImmutableLuthetusTreeViewOptions : ILuthetusTreeViewOptions
{
    public ImmutableLuthetusTreeViewOptions(LuthetusTreeViewOptions luthetusTreeViewOptions)
    {
        InitializeFluxor = luthetusTreeViewOptions.InitializeFluxor;
    }

    public bool InitializeFluxor { get; }
}