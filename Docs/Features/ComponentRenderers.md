# Luthetus.Common (v1.0.0)

## ComponentRenderers

### Table of Contents
- Gifs
- Intent
- Usage
- Comments

---

## Gifs
TODO: Not started writing this yet.

---

## Intent
Provide a way for a `Class Library` to "render" Blazor Components.

This is down by the `Class Library` defining an interface which has a variety of types on it.

Perhaps one wants to render a Counter. Then a property on that interface might be `Type? CounterDisplayRendererType`.

The downstream `Razor Class Library` which is referencing the `Class Library` will then set the `CounterDisplayRendererType` property using dependency injection.

Lastly, [DynamicComponent](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/dynamiccomponent?view=aspnetcore-7.0) is a built in component for Blazor. It takes a `Type` which (*TODO: FACT CHECK THIS*)  must inherit from IComponent.

---

## Usage
TODO: Not started writing this yet.

---

## Comments
TODO: Not started writing this yet.