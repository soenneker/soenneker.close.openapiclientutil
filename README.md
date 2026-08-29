[![](https://img.shields.io/nuget/v/soenneker.close.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.close.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.close.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.close.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.close.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.close.openapiclientutil/)

# Soenneker.Close.OpenApiClientUtil

Exposes a cached OpenAPI client instance.

## Install

```bash
dotnet add package Soenneker.Close.OpenApiClientUtil
```

## Quick start

```csharp
using Soenneker.Close.OpenApiClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddCloseOpenApiClientUtilAsSingleton();
```

Adds `CloseOpenApiClientUtil` as a singleton service.

## What you get

- `ICloseOpenApiClientUtil` — Exposes a cached OpenAPI client instance.
- `CloseOpenApiClientUtilRegistrar` — Registers the OpenAPI client utility for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `CloseOpenApiClientUtilRegistrar.AddCloseOpenApiClientUtilAsSingleton(services)` | Adds `CloseOpenApiClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `CloseOpenApiClientUtilRegistrar.AddCloseOpenApiClientUtilAsScoped(services)` | Adds `CloseOpenApiClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Dispose instances you own when their scope ends so held resources can be released.
