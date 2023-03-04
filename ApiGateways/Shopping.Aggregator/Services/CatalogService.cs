using Shopping.Aggregator.Interfaces;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;
    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<IEnumerable<CatalogModel>> GetCatalog() => throw new NotImplementedException();

    public Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category) => throw new NotImplementedException();

    public Task<CatalogModel> GetCatalog(string id) => throw new NotImplementedException();
}
