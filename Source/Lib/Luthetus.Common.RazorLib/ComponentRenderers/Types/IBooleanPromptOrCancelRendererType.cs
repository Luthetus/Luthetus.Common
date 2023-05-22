﻿namespace Luthetus.Common.RazorLib.ComponentRenderers.Types;

public interface IBooleanPromptOrCancelRendererType
{
    public bool IncludeCancelOption { get; set; }
    public string Message { get; set; }
    public string? AcceptOptionTextOverride { get; set; }
    public string? DeclineOptionTextOverride { get; set; }
    public Action OnAfterAcceptAction { get; set; }
    public Action OnAfterDeclineAction { get; set; }
    public Action OnAfterCancelAction { get; set; }
}