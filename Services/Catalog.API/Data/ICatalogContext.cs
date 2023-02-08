using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data;

/// <summary>
///     The class connection with DB. Add setting DB.
/// </summary>
public interface ICatalogContext
{
    /// <summary>
    ///     Product collection in Db.
    /// </summary>
    IMongoCollection<Product> Products { get; }
}
