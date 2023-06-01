# Luthetus.Common (v1.0.0)

## OutOfBoundsClick

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
Render an html &lt;`div`&gt; that has a Blazor `@onclick` event.

The &lt;`div`&gt; covers the viewport, and has a lower z-index than the html element one wants to track out of bound clicks on.

This is most commonly used for the dropdown Blazor component. That is, render a dropdown, then a div behind that dropdown which covers the viewport. Onclick of the dropdown stop propagation so the div behind it does not fire its `@onclick`. But, when one clicks outside the bounds of the dropdown they'll click the div behind it which then unrenders the dropdown.

---

## Usage
TODO: Not started writing this yet.

---

## Comments
TODO: Not started writing this yet.