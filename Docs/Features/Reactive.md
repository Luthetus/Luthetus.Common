# Luthetus.Common (v1.0.0)

## Reactive

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
I needed to have a throttle for various events that fire often.

An example of such event would be the `@onmousemove` Blazor event for the Text Editor Blazor component.

A throttle in my definition will: listen for events, fire immediately upon first event, start a timer, ignore all events until timer is over, concurrently take the most recent event and throw away the rest. Handle that most recent event. Start the timer again.

---

## Usage
TODO: Not started writing this yet.

---

## Comments
TODO: Not started writing this yet.