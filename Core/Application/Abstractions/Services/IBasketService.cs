using Application.DTOs.Basket;
using Domain.Entities;

namespace Application.Abstractions.Services;

public interface IBasketService
{
    public Task<List<BasketItem>> GetBasketItemsAsync();

    public Task AddItemToBasketAsync(CreateBasketItemDTO model);

    public Task RemoveItemFromBasket(string basketItemId);

    public Task UpdateQuantityAsync(UpdateBasketItemDTO model);

    public Basket GetUserActiveBasket { get; }
}