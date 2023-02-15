using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Commands.CheckoutOrder;

namespace Ordering.API.EventBusConsumers;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILogger<BasketCheckoutConsumer> _logger;

    public BasketCheckoutConsumer(
        IMapper mapper,
        IMediator mediator,
        ILogger<BasketCheckoutConsumer> logger
    )
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
        var id = await _mediator.Send(command).ConfigureAwait(false);
        _logger.LogInformation($"BasketCheckoutEvent consumed successfully. Created Order Id : {id}");
    }
}
