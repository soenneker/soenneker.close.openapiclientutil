using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Close.HttpClients.Abstract;
using Soenneker.Close.OpenApiClientUtil.Abstract;
using Soenneker.Close.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Close.OpenApiClientUtil;

///<inheritdoc cref="ICloseOpenApiClientUtil"/>
public sealed class CloseOpenApiClientUtil : ICloseOpenApiClientUtil
{
    private readonly AsyncSingleton<CloseOpenApiClient> _client;

    public CloseOpenApiClientUtil(ICloseOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<CloseOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Close:ApiKey");
            string authHeaderValueTemplate = configuration["Close:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new CloseOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<CloseOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
