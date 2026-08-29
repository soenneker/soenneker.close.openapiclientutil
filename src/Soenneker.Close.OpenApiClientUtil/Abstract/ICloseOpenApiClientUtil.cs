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
    /// <summary>
    /// Returns the configured close OpenAPI Client used by the Close OpenAPI Client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested close OpenAPI Client.</returns>
    ValueTask<CloseOpenApiClient> Get(CancellationToken cancellationToken = default);
}
