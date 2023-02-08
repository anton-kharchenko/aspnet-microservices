﻿using AutoMapper;
using Discount.gRPC.Entities;
using Discount.gRPC.Protos;
using Discount.gRPC.Repositories;
using Grpc.Core;

namespace Discount.gRPC.Services;

/// <summary>
/// Discount service for gRPC connections
/// </summary>
public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository _repository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="repository">Discount API abstractions for interactions with DB.</param>
    /// <param name="logger">Base logger for writing log information.</param>
    /// <param name="mapper">Mapping service.</param>
    public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    ///  Get the discount by the product name.
    /// </summary>
    /// <param name="request">Get discount request. Generated by the proto files.</param>
    /// <param name="context">Context for a server-side call. Generated by the proto files.</param>
    /// <returns>Coupon model.</returns>
    /// <exception cref="RpcException">If coupon not found will be getting rpc exception</exception>
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _repository.GetDiscountAsync(request.ProductName).ConfigureAwait(false);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product name={request.ProductName} is not found."));

        _logger.LogInformation($"Discount retrieved Product name: {coupon.ProductName}, Amount: {coupon.Amount}, Description: {coupon.Description}");
        return _mapper.Map<CouponModel>(coupon);
    }

    /// <summary>
    /// Create the discount by the coupon.
    /// </summary>
    /// <param name="request">Create discount request. Generated by the proto files.</param>
    /// <param name="context">Context for a server-side call. Generated by the proto files.</param>
    /// <returns>Coupon model.</returns>
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.CouponModel);
        await _repository.CreateDiscountAsync(coupon).ConfigureAwait(false);
        _logger.LogInformation($"Discount is successfully created. ProductName: {coupon.ProductName}");
        return _mapper.Map<CouponModel>(coupon);
    }

    /// <summary>
    /// Update the discount by the coupon.
    /// </summary>
    /// <param name="request">Update discount request. Generated by the proto files.</param>
    /// <param name="context">Context for a server-side call. Generated by the proto files</param>
    /// <returns>Coupon model.</returns>
    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.CouponModel);
        await _repository.UpdateDiscountAsync(coupon).ConfigureAwait(false);
        _logger.LogInformation($"Discount is successfully updated. ProductName: {coupon.ProductName}");
        return _mapper.Map<CouponModel>(coupon);
    }

    /// <summary>
    ///  Delete the discount by the coupon.
    /// </summary>
    /// <param name="request">Delete discount request. Generated by the proto files.</param>
    /// <param name="context">Context for a server-side call. Generated by the proto files</param>
    /// <returns>Delete discount response. Generated by the proto files</returns>
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var deleted = await _repository.DeleteDiscountAsync(request.ProductName).ConfigureAwait(false);
        return new DeleteDiscountResponse
        {
            Success = deleted
        };
    }
}
