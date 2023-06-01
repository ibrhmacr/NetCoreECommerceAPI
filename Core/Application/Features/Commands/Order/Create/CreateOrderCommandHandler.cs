using Application.Abstractions.Hubs;
using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Commands.Order.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest,CreateOrderCommandResponse>
{
    private readonly IOrderService _orderService;
    private readonly IBasketService _basketService;
    private readonly IOrderHubService _orderHubService;

    public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService)
    {
        _orderService = orderService;
        _basketService = basketService;
        _orderHubService = orderHubService;
    }

    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        await _orderService.CreateOrder(new()
        {
            Address = request.Address,
            Description = request.Description,
            BasketId = _basketService.GetUserActiveBasket?.Id.ToString()
        });
        await _orderHubService.OrderAddedMessageAsync("Money Talks");
        
        return new();
    }
}