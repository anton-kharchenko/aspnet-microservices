using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data;

/// <inheritdoc />
public class CatalogContext : ICatalogContext
{
    /// <summary>
    ///     .ctor
    /// </summary>
    /// <param name="configuration">Application configuration</param>
    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
        Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        CatalogContextSeed.SeedData(Products);
    }

    /// <inheritdoc />
    public IMongoCollection<Product> Products { get; }
}