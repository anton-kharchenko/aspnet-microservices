using Shopping.Aggregator.Extensions;
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

    public async Task<IEnumerable<CatalogModel>> GetCatalog()
    {
        var response = await _httpClient.GetAsync("/api/v1/Catalog").ConfigureAwait(false);
        return (await response.ReadContentAs<List<CatalogModel>>().ConfigureAwait(false))!;
    }

    public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
    {
        var response = await _httpClient.GetAsync($"/api/v1/GetProductByCategory/{category}").ConfigureAwait(false);
        return (await response.ReadContentAs<List<CatalogModel>>().ConfigureAwait(false))!;
    }

    public async Task<CatalogModel> GetCatalog(string id)
    {
        var response = await _httpClient.GetAsync($"/api/v1/Catalog/{id}").ConfigureAwait(false);
        return (await response.ReadContentAs<CatalogModel>().ConfigureAwait(false))!;
    }
}
