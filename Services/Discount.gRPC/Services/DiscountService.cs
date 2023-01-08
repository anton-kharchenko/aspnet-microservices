using AutoMapper;
using Discount.gRPC.Entities;
using Discount.gRPC.Protos;
using Discount.gRPC.Repositories;
using Grpc.Core;

namespace Discount.gRPC.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository _repository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public override async Task<CouponModel> GetDiscountAsync(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _repository.GetDiscountAsync(request.ProductName).ConfigureAwait(false);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Discount with product name={request.ProductName} is not found."));
        _logger.LogInformation(
            $"Discount retrieved Product name: {coupon.ProductName}, Amount: {coupon.Amount}, Description: {coupon.Description}");
        return _mapper.Map<CouponModel>(coupon);
    }

    public override async Task<CouponModel> CreateDiscountAsync(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.CouponModel);
        await _repository.CreateDiscountAsync(coupon).ConfigureAwait(false);
        _logger.LogInformation($"Discount is successfully created. ProductName: {coupon.ProductName}");
        return _mapper.Map<CouponModel>(coupon);
    }

    public override async Task<CouponModel> UpdateDiscountAsync(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.CouponModel);
        await _repository.UpdateDiscountAsync(coupon).ConfigureAwait(false);
        _logger.LogInformation($"Discount is successfully updated. ProductName: {coupon.ProductName}");
        return _mapper.Map<CouponModel>(coupon);
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscountAsync(DeleteDiscountRequest request, ServerCallContext context)
    {
        var deleted = await _repository.DeleteDiscountAsync(request.ProductName);
        return  new DeleteDiscountResponse
        {
            Success = deleted
        };
    }
}