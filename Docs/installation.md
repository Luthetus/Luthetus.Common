# BlazorCommon (v1.0.0)

## Installation

### Goal

- Reference the `BlazorCommon` Nuget Package
- Register the `Services`
- Reference the `CSS`
- Reference the `JavaScript`
- In `MainLayout.razor` render the `<BlazorCommon.RazorLib.BlazorCommonInitializer/>` Blazor component

### Steps
- Reference the `BlazorCommon` NuGet Package

Use your preferred way to install NuGet Packages to install `BlazorCommon`.

The nuget.org link to the NuGet Package is here: https://www.nuget.org/packages/BlazorCommon

- Register the `Services`

In my C# Project services are registered in `Program.cs`

Go to the file that you register your services and add the following lines of C# code.

```csharp
using BlazorCommon.RazorLib;

builder.Services.AddBlazorCommonServices();
```

- Reference the `CSS`

In my C# Project CSS files are referenced from `Pages/_Layout.cshtml`

Go to the file that you reference CSS files from and add the following CSS reference.

```html
    <link href="_content/BlazorCommon.RazorLib/blazorCommon.css" rel="stylesheet" />
```

- Reference the `JavaScript`

In my C# Project JavaScript files are referenced from `Pages/_Layout.cshtml`

Go to the file that you reference JavaScript files from and add the following JavaScript reference below the Blazor framework JavaScript reference

```html
    <script src="_content/BlazorCommon.RazorLib/blazorCommon.js"></script>
```

- In `MainLayout.razor` render the `<BlazorCommon.RazorLib.BlazorCommonInitializer/>` Blazor component

In `MainLayout.razor` find the top most HTML element which wraps around the website content. As child content of the top most HTML element render the `<BlazorCommon.RazorLib.BlazorCommonInitializer/>`. The initializer needs to be cascaded any css variables which are to be used.

So, to cascade the css variables dependency inject `IAppOptionsService`. I will make a code behind to put this code, but the @code section would work as well.

```csharp
[Inject]
private IAppOptionsService AppOptionsService { get; set; } = null!;
```

Next we can subscribe to the `AppOptionsService.AppOptionsStateWrap.StateChanged` event. This allows us to re-render when the theme is changed.

Implement the interface `IDisposable`.

In the `Dispose()` method unsubscribe an unimplemented method from the `AppOptionsService.AppOptionsStateWrap.StateChanged` event.

Override the Blazor Lifecycle method named: `OnInitialized()`.

In the `OnInitialized()` method subscribe an unimplemented method to the `AppOptionsService.AppOptionsStateWrap.StateChanged` event.

I subscribed and unsubscribed with a method I made named: `AppOptionsStateWrapOnStateChanged(...)`.

The following is code snippet is the entirety of my `MainLayout.razor.cs` as of this step.

```csharp
using BlazorCommon.RazorLib.Options;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Shared;

public partial class MainLayout : LayoutComponentBase, IDisposable
{
    [Inject]
    private IAppOptionsService AppOptionsService { get; set; } = null!;

    protected override void OnInitialized()
    {
        AppOptionsService.AppOptionsStateWrap.StateChanged += AppOptionsStateWrapOnStateChanged;
        
        base.OnInitialized();
    }

    private void AppOptionsStateWrapOnStateChanged(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
    
    public void Dispose()
    {
        AppOptionsService.AppOptionsStateWrap.StateChanged -= AppOptionsStateWrapOnStateChanged;
    }
}
```

The method which we subscribe and unscribe to the event `AppOptionsService.AppOptionsStateWrap.StateChanged` needs to re-render the main-layout. Therefore, the method's signature needs to change. Make the method `async void`.

The implementation of the method is a single line: `await InvokeAsync(StateHasChanged);`. The following code snippet it my method I subscribe and unsubscribe with.

```csharp
private async void AppOptionsStateWrapOnStateChanged(object? sender, EventArgs e)
{
    await InvokeAsync(StateHasChanged);
}
```

In order to ensure the CSS variables cascade, we must wrap the website's content in an HTML element which has the class of `@AppOptionsService.ThemeCssClassString`.

As of this step, my `MainLayout.razor.cs` is shown in the following code snippet.

```html
@inherits LayoutComponentBase

<PageTitle>BlazorApp1</PageTitle>

<div class="page @AppOptionsService.ThemeCssClassString">
    
    <!-- Content goes here -->
</div>
```

# Next tutorial: [Usage](/Documentation/10_USAGE.md)