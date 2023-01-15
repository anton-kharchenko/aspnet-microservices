using MediatR;
using Ordering.Application.ViewModels;

namespace Ordering.Application.Features.Queries.GetOrdersList;

public class GetOrdersListQuery : IRequest<List<OrdersViewModel>>
{
    public string UserName { get; set; }

    public GetOrdersListQuery(string userName)
    {
        UserName = userName;
    }
}