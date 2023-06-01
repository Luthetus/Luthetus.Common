# Luthetus.Common (v1.0.0)

## Drag

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
When a user performs a `@onmousedown` Blazor event. They can use the [Fluxor (github)](https://github.com/mrpmorris/Fluxor) `IDispatcher Dispatcher` to dispatch a `DragState.SetDragStateAction`. Likely they'll decide to show the DragDisplay Blazor component in this situation which looks like:

```csharp
Dispatcher.Dispatch(new DragState.SetDragStateAction(true, mouseEventArgs));
```

Now that the DragDisplay is rendered, it appears as an invisible cover across the entire application. More specifically, its a &lt;`div`&gt; that takes up 100% of the viewport.

This &lt;`div`&gt; has a `@onmousemove` Blazor event.

The `@onmousemove` event will notify any Blazor components which are subscribed to the drag state that a `@onmousemove` event occurred.

The subscribers have an event handler implementation which then does a diff between their instance level field `_previousMouseEventArgs` and the event handler's mouse event to calculate the distance dragged.

---

## Usage
TODO: Not started writing this yet.

---

## Comments
TODO: Not started writing this yet.