using Catalog.API.Entities;

namespace Catalog.API.Repositories;

/// <summary>
///     Working with product table. Data base - MongoDb.
///     API layer for communication between the application and DB. Implementation of Repository pattern.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    ///     Gets all products
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Product>> GetProductsAsync();

    /// <summary>
    ///     Gets the product associated with the id.
    /// </summary>
    /// <param name="id">Id of product.</param>
    /// <returns>The product.</returns>
    Task<Product?> GetProductByIdAsync(string id);

    /// <summary>
    ///     Gets the product associated with the product name.
    /// </summary>
    /// <param name="name">The product name.</param>
    /// <returns>The list of products.</returns>
    Task<IEnumerable<Product>> GetProductByNameAsync(string name);

    /// <summary>
    ///     Gets the product associated with the product name.
    /// </summary>
    /// <param name="categoryName">The product category name.</param>
    /// <returns>The list of products.</returns>
    Task<IEnumerable<Product>> GetProductByCategoryNameAsync(string categoryName);

    /// <summary>
    ///     Create a new product
    /// </summary>
    /// <param name="product">The body of product</param>
    Task CreateProductAsync(Product product);

    /// <summary>
    ///     Update the exist product
    /// </summary>
    /// <param name="product">Update the product</param>
    /// <returns>Status of operation</returns>
    Task<bool> UpdateProductAsync(Product product);

    /// <summary>
    ///     Delete the exist product
    /// </summary>
    /// <param name="id">The product id</param>
    /// <returns>Status of operation</returns>
    Task<bool> DeleteProductAsync(string id);
}
