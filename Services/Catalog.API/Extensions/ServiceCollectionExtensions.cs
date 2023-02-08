using Catalog.API.Data;
using Catalog.API.Repositories;

namespace Catalog.API.Extensions;

/// <summary>
///     Contains application service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Add connection with Db and repository is layer for interaction.
    /// </summary>
    /// <param name="services">The service collection</param>
    public static void AddContext(this IServiceCollection services)
    {
        services.AddScoped<ICatalogContext, CatalogContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
