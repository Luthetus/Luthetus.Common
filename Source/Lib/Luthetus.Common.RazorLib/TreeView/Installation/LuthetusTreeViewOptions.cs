﻿namespace Luthetus.Common.RazorLib.TreeView.Installation;

public class LuthetusTreeViewOptions : ILuthetusTreeViewOptions
{
    /// <summary>
    /// If one already is using Fluxor they can set this property
    /// to false and then in
    ///
    /// AddFluxor(options => options.ScanAssemblies(...))
    ///
    /// Include typeof(LuthetusTreeViewOptions).Assembly
    /// when invoking ScanAssemblies for AddFluxor
    /// service collection extension method
    /// </summary>
    public bool InitializeFluxor { get; set; } = true;
}