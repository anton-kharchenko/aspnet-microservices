using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models.Email;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Commands.CheckoutOrder;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService,
        ILogger<CheckoutOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map<Order>(request);
        var order = await _orderRepository.AddAsync(orderEntity).ConfigureAwait(false);

        _logger.LogInformation($"Order {order.Id} is successfully created.");
        await SendMailAsync(order).ConfigureAwait(false);

        return order.Id;
    }

    private async Task SendMailAsync(Order order)
    {
        var email = new Email
        {
            To = "gumb1t97@gmail.com",
            Body = "Order was created.",
            Subject = "Order was created"
        };

        try
        {
            await _emailService.SendEmailAsync(email).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
        }
    }
}
