using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _catalogContext;

    public ProductRepository(ICatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _catalogContext.Products.Find(p => true).ToListAsync().ConfigureAwait(false);
    }

    public async Task<Product> GetProductAsync(string id)
    {
        return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
    {
        var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);

        return await _catalogContext.Products.Find(filter).ToListAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

        return await _catalogContext.Products.Find(filter).ToListAsync().ConfigureAwait(false);
    }

    public async Task CreateProductAsync(Product product)
    {
        await _catalogContext.Products.InsertOneAsync(product).ConfigureAwait(false);
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var uprateResult = await _catalogContext.Products.ReplaceOneAsync(p => p.Id == product.Id, product);

        return uprateResult.IsAcknowledged && uprateResult.MatchedCount > 0;
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        var deleteResult = await _catalogContext.Products.DeleteOneAsync(filter).ConfigureAwait(false);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
}