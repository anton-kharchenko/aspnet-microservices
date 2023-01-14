using AutoMapper;
using Ordering.Application.Features.Commands.CheckoutOrder;
using Ordering.Application.ViewModels;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrdersViewModel>().ReverseMap();
        CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
    }
}