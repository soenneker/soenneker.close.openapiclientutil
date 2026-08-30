using System;
using System.Net.Http;
using System.Text;
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

/// <inheritdoc cref="ICloseOpenApiClientUtil"/>
public sealed class CloseOpenApiClientUtil : ICloseOpenApiClientUtil
{
    private readonly AsyncSingleton<ClientState> _client;

    public CloseOpenApiClientUtil(ICloseOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<ClientState>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Close:ApiKey");
            string authHeaderName = configuration["Close:AuthHeaderName"] ?? "Authorization";
            string? authHeaderValueTemplate = configuration["Close:AuthHeaderValueTemplate"];
            string authHeaderValue = authHeaderValueTemplate is null
                ? $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:"))}"
                : authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(
                new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
                httpClient: httpClient);

            return new ClientState(new CloseOpenApiClient(requestAdapter), requestAdapter);
        });
    }

    public async ValueTask<CloseOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        ClientState state = await _client.Get(cancellationToken).NoSync();
        return state.Client;
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }

    private sealed class ClientState : IDisposable
    {
        private readonly HttpClientRequestAdapter _requestAdapter;

        public CloseOpenApiClient Client { get; }

        public ClientState(CloseOpenApiClient client, HttpClientRequestAdapter requestAdapter)
        {
            Client = client;
            _requestAdapter = requestAdapter;
        }

        public void Dispose()
        {
            _requestAdapter.Dispose();
        }
    }
}
