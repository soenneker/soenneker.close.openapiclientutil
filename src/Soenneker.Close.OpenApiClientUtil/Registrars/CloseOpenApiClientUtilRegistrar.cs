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
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddCloseOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddCloseOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ICloseOpenApiClientUtil, CloseOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="CloseOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddCloseOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddCloseOpenApiHttpClientAsScoped()
                .TryAddScoped<ICloseOpenApiClientUtil, CloseOpenApiClientUtil>();

        return services;
    }
}
