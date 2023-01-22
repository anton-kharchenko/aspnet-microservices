using AutoMapper;
using Discount.gRPC.Entities;
using Discount.gRPC.Protos;

namespace Discount.gRPC.Mapper;

/// <summary>
/// Discount mapping model
/// </summary>
public class DiscountProfile : Profile
{
    /// <summary>
    /// .ctor
    /// </summary>
    public DiscountProfile()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();
    }
}
