using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Close.HttpClients.Registrars;
using Soenneker.Close.OpenApiClientUtil.Abstract;

namespace Soenneker.Close.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class CloseOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="CloseOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddCloseOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddCloseOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ICloseOpenApiClientUtil, CloseOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="CloseOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddCloseOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddCloseOpenApiHttpClientAsSingleton()
                .TryAddScoped<ICloseOpenApiClientUtil, CloseOpenApiClientUtil>();

        return services;
    }
}
