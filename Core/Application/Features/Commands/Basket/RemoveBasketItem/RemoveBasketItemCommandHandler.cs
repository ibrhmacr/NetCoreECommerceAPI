using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Commands.Basket.RemoveBasketItem;

public class RemoveBasketItemCommandHandler : IRequestHandler<RemoveBasketItemCommandRequest, RemoveBasketItemCommandResponse>
{
    private readonly IBasketService _basketService;

    public RemoveBasketItemCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<RemoveBasketItemCommandResponse> Handle(RemoveBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketService.RemoveItemFromBasket(request.BasketItemId);
        return new();
    }
}



