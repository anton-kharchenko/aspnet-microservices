using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Application.Features.Commands.UpdateOrder;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper,
        ILogger<UpdateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToDelete = await _orderRepository.GetByIdAsync(request.Id).ConfigureAwait(false);
        if (orderToDelete == null)
        {
            _logger.LogError("Order not exists on database.");
            throw new NotFoundException(nameof(Order), request.Id);
        }

        await DeleteOrderById(orderToDelete).ConfigureAwait(false);

        return Unit.Value;
    }

    private async Task DeleteOrderById(Order orderToDelete)
    {
        await _orderRepository.DeleteAsync(orderToDelete).ConfigureAwait(false);
        _logger.LogInformation($"Order {orderToDelete.Id} is successfully deleted.");
    }
}
