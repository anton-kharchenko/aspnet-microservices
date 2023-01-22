using AutoFixture;
using Discount.API.Controllers;
using Discount.API.Entities;
using Discount.API.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Discount.API.Tests.Controllers;

public class DiscountControllerTests
{
    private readonly DiscountController _discountController;
    private readonly IFixture _fixture;
    private readonly Mock<IDiscountRepository> _discountRepository;

    public DiscountControllerTests()
    {
        _fixture = new Fixture();
        _discountRepository = _fixture.Freeze<Mock<IDiscountRepository>>();
        _discountController = new DiscountController(_discountRepository.Object);
    }

    [Fact]
    public async Task GetDiscountShouldReturnCouponByProductName()
    {
        var discount = _fixture.Create<string>();
        var coupon = _fixture.Create<Coupon>();
        _discountRepository.Setup(x => x.GetDiscountAsync(discount)).ReturnsAsync(coupon);

        var result = await _discountController.GetDiscountAsync(discount).ConfigureAwait(false);

        result.Should().NotBeNull();
        result.Should().BeAssignableTo<ActionResult<Coupon>>();
        result.Result.Should().BeAssignableTo<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value
            .Should()
            .NotBeNull()
            .And.BeOfType(coupon.GetType());
        _discountRepository.Verify(e => e.GetDiscountAsync(discount), Times.Once);
    }

    [Fact]
    public async Task CreateDiscountShouldReturnNewCoupon()
    {
        var coupon = _fixture.Create<Coupon>();
        _discountRepository.Setup(x => x.CreateDiscountAsync(coupon)).ReturnsAsync(true);

        var result = await _discountController.CreateDiscountAsync(coupon).ConfigureAwait(false);

        result.Should().NotBeNull();
        result.Should().BeAssignableTo<ActionResult<Coupon>>();
        _discountRepository.Verify(e => e.CreateDiscountAsync(coupon), Times.Once);
    }

    [Fact]
    public async Task UpdateDiscountShouldReturnUpdatedCoupon()
    {
        var coupon = _fixture.Create<Coupon>();
        _discountRepository.Setup(x => x.UpdateDiscountAsync(coupon)).ReturnsAsync(true);

        var result = await _discountController.UpdateDiscountAsync(coupon).ConfigureAwait(false);

        result.Should().NotBeNull();
        result.Should().BeAssignableTo<ActionResult<Coupon>>();
        result.Result.Should().BeAssignableTo<OkObjectResult>();
        _discountRepository.Verify(e => e.UpdateDiscountAsync(coupon), Times.Once);
    }

    [Fact]
    public async Task DeleteDiscountShouldReturnNewCoupon()
    {
        var couponName = _fixture.Create<string>();
        _discountRepository.Setup(x => x.DeleteDiscountAsync(couponName)).ReturnsAsync(true);

        var result = await _discountController.DeleteDiscountAsync(couponName).ConfigureAwait(false);

        result.Should().NotBeNull();
        result.Should().BeAssignableTo<ActionResult<bool>>();
        result.Result.Should().BeAssignableTo<OkObjectResult>();
        _discountRepository.Verify(e => e.DeleteDiscountAsync(couponName), Times.Once);
    }
}
