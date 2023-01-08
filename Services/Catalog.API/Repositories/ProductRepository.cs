using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

/// <inheritdoc />
public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _catalogContext;

    /// <summary>
    ///     .ctor
    /// </summary>
    /// <param name="catalogContext">The Context of data base</param>
    public ProductRepository(ICatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _catalogContext.Products.Find(p => true).ToListAsync().ConfigureAwait(false);
    }
    
    /// <inheritdoc />
    public async Task<Product?> GetProductByIdAsync(string id)
    {
        return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
    {
        var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);

        return await _catalogContext.Products.Find(filter).ToListAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> GetProductByCategoryNameAsync(string categoryName)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

        return await _catalogContext.Products.Find(filter).ToListAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task CreateProductAsync(Product product)
    {
        await _catalogContext.Products.InsertOneAsync(product).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<bool> UpdateProductAsync(Product product)
    {
        var uprateResult = await _catalogContext.Products.ReplaceOneAsync(p => p.Id == product.Id, product);

        return uprateResult.IsAcknowledged && uprateResult.MatchedCount > 0;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteProductAsync(string id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        var deleteResult = await _catalogContext.Products.DeleteOneAsync(filter).ConfigureAwait(false);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
}