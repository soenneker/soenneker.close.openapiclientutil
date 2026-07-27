using Soenneker.Close.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Close.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface ICloseOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<CloseOpenApiClient> Get(CancellationToken cancellationToken = default);
}
