namespace Luthetus.Common.RazorLib.ComponentRenderers.Types;

public interface IErrorNotificationRendererType
{
    public const string CSS_CLASS_STRING = "luth_error";
    public string Message { get; set; }
}
