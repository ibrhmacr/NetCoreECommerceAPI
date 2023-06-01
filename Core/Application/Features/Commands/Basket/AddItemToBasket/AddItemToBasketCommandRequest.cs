using MediatR;

namespace Application.Features.Commands.Basket.AddItemToBasket;

public class AddItemToBasketCommandRequest : IRequest<AddItemToBasketCommandResponse>
{
    public string ProductId { get; set; }

    public int Quantity { get; set; }
}