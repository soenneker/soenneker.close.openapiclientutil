[![](https://img.shields.io/nuget/v/soenneker.close.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.close.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.close.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.close.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.close.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.close.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.close.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.close.openapiclientutil/actions/workflows/codeql.yml)

# Soenneker.Close.OpenApiClientUtil

Provides an authenticated, cached instance of the Kiota-generated Close CRM API client.

## Installation

```bash
dotnet add package Soenneker.Close.OpenApiClientUtil
```

## API-key configuration

```json
{
  "Close": {
    "ApiKey": "<Close API key>",
    "ClientBaseUrl": "https://api.close.com/api/v1"
  }
}
```

`Close:ApiKey` is required. The utility uses Close's HTTP Basic API-key scheme by default: the key is the username and the password is empty. See [Close's authentication documentation](https://developer.close.com/api/overview/api-key-authentication).

For OAuth, place the access token in `Close:ApiKey` and set `Close:AuthHeaderValueTemplate` to `Bearer {token}`. `Close:AuthHeaderName` can override the default `Authorization` header name.

Keep API keys and OAuth tokens in a secret provider rather than source control.

## Registration

```csharp
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Close.OpenApiClientUtil.Registrars;

services.AddCloseOpenApiClientUtilAsSingleton();
```

Use `AddCloseOpenApiClientUtilAsScoped()` when each dependency-injection scope should own an isolated generated client, request adapter, and HTTP-client cache entry.

## Usage

```csharp
using Soenneker.Close.OpenApiClient.Me;
using Soenneker.Close.OpenApiClientUtil.Abstract;

public sealed class CloseProfileService
{
    private readonly ICloseOpenApiClientUtil _closeClients;

    public CloseProfileService(ICloseOpenApiClientUtil closeClients)
    {
        _closeClients = closeClients;
    }

    public async ValueTask<MeGetResponse?> GetCurrentUser(
        CancellationToken cancellationToken)
    {
        var close = await _closeClients.Get(cancellationToken);

        return await close.Me.GetAsync(
            request =>
            {
                request.QueryParameters.Fields = "id,name,organization_id";
            },
            cancellationToken);
    }
}
```

Other root request builders include `Lead`, `Contact`, `Opportunity`, `Activity`, `Task`, `Sequence`, and `Webhook`. Names follow the generated OpenAPI surface.

## Lifecycle and behavior

- The first `Get` creates the HTTP client, Kiota request adapter, and generated client. Later calls on the same utility return that client.
- Configuration and credentials are captured during first initialization. Recreate the owning scope or application instance after rotating them.
- The token passed to `Get` cancels initialization only. Pass a cancellation token to each generated endpoint call.
- Let dependency injection dispose `ICloseOpenApiClientUtil`. Disposal releases its request adapter and scoped HTTP-client entry.
- Generated endpoint methods may return `null` when the schema permits an empty response.
- Service failures are surfaced through generated error models or Kiota exceptions according to each endpoint's schema mapping.
- Request builders and models can change when the generated-client package is refreshed from Close's OpenAPI description.
