# Luthetus.Common (v1.0.0)

## BackgroundTaskCase

### Table of Contents
- Gifs
- Intent
- Usage
- Comments

---

## Gifs
TODO: (2023-06-01)

---

## Intent
`BackgroundTaskCase` is a folder containing code used to provide an [IHostedService (MS Docs)](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-7.0&tabs=visual-studio)  for `Luthetus.Ide`.

Example: creating a new .NET Solution using the command line. I could use a [fire and forget task (stackoverflow)](https://stackoverflow.com/questions/60778423/fire-and-forget-using-task-run-or-just-calling-an-async-method-without-await). But, a fire and forget task will:
- Fail silently when there is an exception.
- At times never complete execution. Perhaps the application was shutdown. The runtime has no knowledge of the task. Therefore, the runtime will not wait for that fire and forget task to finish before the process ends.

To contrast fire and forget tasks, an [IHostedService (MS Docs)](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-7.0&tabs=visual-studio) is managed by the runtime. One can implement a method named `virtual async Task BackgroundService.StopAsync(...)`. This method will then be called when a graceful shutdown of the application is done. Allowing you to decide on what actions to take in the case of a graceful shutdown.

A side note would be mentioning [Hangfire](https://www.hangfire.io/). In the future, perhaps [Hangfire](https://www.hangfire.io/) would be a good addition to `Luthetus.Ide`. As, [Hangfire](https://www.hangfire.io/) lets one persist background jobs to a database. One reason for this being useful is when a job fails, you can restart that job because all the parameters were stored in the database using serialization.

---

## Usage
TODO: (2023-06-01)

---

## Comments
TODO: (2023-06-01)