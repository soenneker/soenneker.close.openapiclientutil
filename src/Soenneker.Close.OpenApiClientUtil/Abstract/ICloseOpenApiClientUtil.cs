using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Close.OpenApiClient;

namespace Soenneker.Close.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a configured, reusable Close OpenAPI client.
/// </summary>
public interface ICloseOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached generated client for this utility instance.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel client initialization.</param>
    /// <returns>An authenticated Close OpenAPI client.</returns>
    ValueTask<CloseOpenApiClient> Get(CancellationToken cancellationToken = default);
}
