using Discount.gRPC.Protos;

namespace Basket.API.Services;

/// <summary>
/// Grpc Service class for getting discount
/// </summary>
public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="discountProtoServiceClient"></param>
    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
    {
        _discountProtoServiceClient = discountProtoServiceClient;
    }

    /// <summary>
    /// Get Discount for product by product name
    /// </summary>
    /// <param name="productName">The product name that need to get the discount</param>
    /// <returns>Model of discount for this product</returns>
    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequest = new GetDiscountRequest { ProductName = productName };
        return await _discountProtoServiceClient.GetDiscountAsync(discountRequest);
    }
}