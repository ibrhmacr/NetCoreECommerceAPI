using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Queries.Basket.GetBasketItems;

public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest,List<GetBasketItemsQueryResponse>>
{
    private readonly IBasketService _basketService;

    public GetBasketItemsQueryHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
    {
        var basketItems = await _basketService.GetBasketItemsAsync();
        return basketItems.Select(ba => new GetBasketItemsQueryResponse()
        {
            BasketItemId = ba.Id.ToString(),
            ProductName = ba.Product.Name,
            Price = ba.Product.Price,
            Quantity = ba.Quantity

        }).ToList();
    }
}