using Fluxor;
using Luthetus.Common.RazorLib.Drag;
using Luthetus.Common.RazorLib.Store.ApplicationOptions;
using Luthetus.Common.RazorLib.Store.DialogCase;
using Luthetus.Common.RazorLib.Store.DropdownCase;
using Luthetus.Common.RazorLib.Store.NotificationCase;
using Luthetus.Common.RazorLib.Store.ThemeCase;
using Luthetus.Common.RazorLib.Store.TreeViewCase;
using Luthetus.Common.RazorLib.BackgroundTaskCase;
using Luthetus.Common.RazorLib.Clipboard;
using Luthetus.Common.RazorLib.ComponentRenderers;
using Luthetus.Common.RazorLib.Dialog;
using Luthetus.Common.RazorLib.Dropdown;
using Luthetus.Common.RazorLib.Notification;
using Luthetus.Common.RazorLib.Options;
using Luthetus.Common.RazorLib.Storage;
using Luthetus.Common.RazorLib.Theme;
using Luthetus.Common.RazorLib.TreeView;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace Luthetus.Common.RazorLib;

public record LuthetusCommonFactories
{
    public Func<IServiceProvider, IDragService> DragServiceFactory { get; init; } =
        serviceProvider => new DragService(true);

    public Func<IServiceProvider, IClipboardService> ClipboardServiceFactory { get; init; } =
        serviceProvider => new JavaScriptInteropClipboardService(
            true,
            serviceProvider.GetRequiredService<IJSRuntime>());

    public Func<IServiceProvider, IDialogService> DialogServiceFactory { get; init; } =
        serviceProvider => new DialogService(
            true,
            serviceProvider.GetRequiredService<IDispatcher>(),
            serviceProvider.GetRequiredService<IState<DialogRecordsCollection>>());

    public Func<IServiceProvider, INotificationService> NotificationServiceFactory { get; init; } =
        serviceProvider => new NotificationService(
            true,
            serviceProvider.GetRequiredService<IDispatcher>(),
            serviceProvider.GetRequiredService<IState<NotificationRecordsCollection>>());

    public Func<IServiceProvider, IDropdownService> DropdownServiceFactory { get; init; } =
        serviceProvider => new DropdownService(
            true,
            serviceProvider.GetRequiredService<IDispatcher>(),
            serviceProvider.GetRequiredService<IState<DropdownsState>>());

    public Func<IServiceProvider, IStorageService> StorageServiceFactory { get; init; } =
        serviceProvider => new LocalStorageService(
            true,
            serviceProvider.GetRequiredService<IJSRuntime>());

    public Func<IServiceProvider, IAppOptionsService> AppOptionsServiceFactory { get; init; } =
        serviceProvider => new AppOptionsService(
            true,
            serviceProvider.GetRequiredService<IState<AppOptionsState>>(),
            serviceProvider.GetRequiredService<IState<ThemeRecordsCollection>>(),
            serviceProvider.GetRequiredService<IDispatcher>(),
            serviceProvider.GetRequiredService<IStorageService>());

    public Func<IServiceProvider, IThemeService> ThemeServiceFactory { get; init; } =
        serviceProvider => new ThemeService(
            true,
            serviceProvider.GetRequiredService<IState<ThemeRecordsCollection>>(),
            serviceProvider.GetRequiredService<IDispatcher>());

    public Func<IServiceProvider, ITreeViewService> TreeViewServiceFactory { get; init; } =
        serviceProvider => new TreeViewService(
            true,
            serviceProvider.GetRequiredService<IState<TreeViewStateContainer>>(),
            serviceProvider.GetRequiredService<IDispatcher>(),
            serviceProvider.GetRequiredService<IBackgroundTaskQueue>(),
    serviceProvider.GetRequiredService<ILuthetusCommonComponentRenderers>());
}