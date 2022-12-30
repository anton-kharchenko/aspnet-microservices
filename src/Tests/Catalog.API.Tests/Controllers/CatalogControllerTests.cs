using AutoFixture;
using Catalog.API.Controllers;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Catalog.API.Tests.Controllers;

public class CatalogControllerTests
{
    private readonly CatalogController _catalogController;
    private readonly IFixture _fixture;
    private readonly Mock<ILogger<CatalogController>> _logger;
    private readonly Mock<IProductRepository> _productRepository;

    public CatalogControllerTests()
    {
        _fixture = new Fixture();
        _productRepository = _fixture.Freeze<Mock<IProductRepository>>();
        _logger = _fixture.Freeze<Mock<ILogger<CatalogController>>>();
        _catalogController = new CatalogController(_productRepository.Object, _logger.Object);
    }

    [Fact]
    public async Task GetProductsShouldReturnOnResponseWhenDataFound()
    {
        // Arrange
        var products = _fixture.Create<IEnumerable<Product>>();
        _productRepository.Setup(x => x.GetProductsAsync()).ReturnsAsync(products);

        // Act

        var result = await _catalogController.GetProducts().ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<ActionResult<IEnumerable<Product>>>();
        result.Result.Should().BeAssignableTo<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value
            .Should()
            .NotBeNull()
            .And.BeOfType(products.GetType());
        _productRepository.Verify(x => x.GetProductsAsync(), Times.Once);
    }
}