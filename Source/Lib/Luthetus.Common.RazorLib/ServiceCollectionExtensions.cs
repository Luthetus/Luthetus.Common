using Luthetus.Common.RazorLib.Clipboard;
using Luthetus.Common.RazorLib.Dialog;
using Luthetus.Common.RazorLib.Drag;
using Luthetus.Common.RazorLib.Dropdown;
using Luthetus.Common.RazorLib.Notification;
using Luthetus.Common.RazorLib.Options;
using Luthetus.Common.RazorLib.Storage;
using Luthetus.Common.RazorLib.Theme;
using Luthetus.Common.RazorLib.TreeView;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;

namespace Luthetus.Common.RazorLib;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// The <see cref="configure"/> parameter provides an instance of a record type. Use the 'with' keyword to
    /// change properties and then return the new instance.
    /// </summary>
    public static IServiceCollection AddLuthetusCommonServices(
        this IServiceCollection services,
        Func<LuthetusCommonOptions, LuthetusCommonOptions>? configure = null)
    {
        var luthetusCommonOptions = new LuthetusCommonOptions();

        if (configure is not null)
            luthetusCommonOptions = configure.Invoke(luthetusCommonOptions);

        services
            .AddSingleton(luthetusCommonOptions)
            .AddScoped(serviceProvider =>
                luthetusCommonOptions.LuthetusCommonFactories.ClipboardServiceFactory.Invoke(serviceProvider))
            .AddScoped(serviceProvider =>
                luthetusCommonOptions.LuthetusCommonFactories.DialogServiceFactory.Invoke(serviceProvider))
            .AddScoped(serviceProvider =>
                luthetusCommonOptions.LuthetusCommonFactories.NotificationServiceFactory.Invoke(serviceProvider))
            .AddScoped(serviceProvider =>
                luthetusCommonOptions.LuthetusCommonFactories.DragServiceFactory.Invoke(serviceProvider))
            .AddScoped(serviceProvider =>
                luthetusCommonOptions.LuthetusCommonFactories.DropdownServiceFactory.Invoke(serviceProvider))
            .AddScoped(serviceProvider =>
                luthetusCommonOptions.LuthetusCommonFactories.AppOptionsServiceFactory.Invoke(serviceProvider))
            .AddScoped(serviceProvider =>
                luthetusCommonOptions.LuthetusCommonFactories.StorageServiceFactory.Invoke(serviceProvider))
            .AddScoped(serviceProvider =>
                luthetusCommonOptions.LuthetusCommonFactories.ThemeServiceFactory.Invoke(serviceProvider))
            .AddScoped(serviceProvider =>
                luthetusCommonOptions.LuthetusCommonFactories.TreeViewServiceFactory.Invoke(serviceProvider));

        if (luthetusCommonOptions.InitializeFluxor)
        {
            services.AddFluxor(options =>
                options.ScanAssemblies(
                    typeof(ServiceCollectionExtensions).Assembly));
        }

        return services;
    }
}