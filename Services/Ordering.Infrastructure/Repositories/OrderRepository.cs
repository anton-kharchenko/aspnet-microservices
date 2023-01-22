#region

using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Data.Context;
using Ordering.Infrastructure.Repository;

#endregion

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(OrderContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName)
    {
        return await DbContext.Orders!.Where(o => o.UserName == userName).ToListAsync().ConfigureAwait(false);
    }
}
