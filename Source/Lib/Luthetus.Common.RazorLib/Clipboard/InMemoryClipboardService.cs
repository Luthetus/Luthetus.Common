﻿namespace Luthetus.Common.RazorLib.Clipboard;

public class InMemoryClipboardService : IClipboardService
{
    private string _clipboard = string.Empty;

    public InMemoryClipboardService(
        bool isEnabled)
    {
        IsEnabled = isEnabled;
    }

    public bool IsEnabled { get; }

    public Task<string> ReadClipboard()
    {
        return Task.FromResult(_clipboard);
    }

    public Task SetClipboard(string value)
    {
        _clipboard = value;
        return Task.CompletedTask;
    }
}