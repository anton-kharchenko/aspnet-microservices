﻿using Discount.gRPC.Protos;

namespace Basket.API.Services;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
    {
        _discountProtoServiceClient = discountProtoServiceClient;
    }

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequest = new GetDiscountRequest { ProductName = productName };
        return await _discountProtoServiceClient.GetDiscountAsync(discountRequest);
    }
}