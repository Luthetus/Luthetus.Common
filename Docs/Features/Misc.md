# Luthetus.Common (v1.0.0)

## Misc

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
I use `RenderStateKey` to avoid rendering a Blazor component.

One can override the Blazor lifecycle method `override bool ShouldRender()` for example.

Then track a class level field of `RenderStateKey` named `previousRenderStateKey`

In `override bool ShouldRender()` go on to check if the `RenderStateKey` changed.
If so, then return true to re-render (be sure to update `previousRenderStateKey`).

Otherwise you can return false to skip that render.

---

## Usage
TODO: Not started writing this yet.

---

## Comments
TODO: Not started writing this yet.