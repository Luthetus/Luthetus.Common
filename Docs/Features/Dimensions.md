# Luthetus.Common (v1.0.0)

## Dimensions

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
Used write CSS with C#. More specifically,
this is used to interpolate various C# state
into a CSS style string.

So for example: a C# class might have a `double Width` property on it. We want the style string for an HTML element to recieve this Width properties value.

style='width: `@(_model.Width.ToCssValue())`px;'

```razor
<div style='width: @(_model.Width.ToCssValue())px;'>
</div>
```

In C# we can animate the element by saying:

```csharp
private void IncreaseWidthOnClick()
{
    _model.Width++;
}
```

We need a button to click:

```razor
<button @onclick="IncreaseWidthOnClick">
    Increase Width
</button>
```

Lastly, `@onclick` receives an EventCallback. Essentially this is an Action that invokes `StateHasChanged` on the component that supplies the value. So, `IncreaseWidthOnClick` does not need to have `StateHasChanged()` prior to returning because the EventCallback logic will implicitly invoke it for us once our method finishes.

---

## Usage
TODO: Not started writing this yet.

---

## Comments
TODO: Not started writing this yet.