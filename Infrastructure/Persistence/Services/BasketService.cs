using Application.Abstractions.Services;
using Application.DTOs.Basket;
using Application.Repositories;
using Application.Repositories.Basket;
using Application.Repositories.BasketItem;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

public class BasketService : IBasketService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IBasketWriteRepository _basketWriteRepository;
    private readonly IBasketItemWriteRepository _basketItemWriteRepository;
    private readonly IBasketItemReadRepository _basketItemReadRepository;
    //Sepete Eklenecek urun var ise quantitysini
    //uzerine yazmak gerekir. Bu yuzden ekleme islemi sirasinda
    //bu durumunda kontrolu yapilmalidir. Bu yuzden bu repository i kullaniyoruz

    public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketReadRepository basketReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketItemWriteRepository basketItemWriteRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _orderReadRepository = orderReadRepository;
        _basketReadRepository = basketReadRepository;
        _basketWriteRepository = basketWriteRepository;
        _basketItemReadRepository = basketItemReadRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
    }
    
    public async Task<List<BasketItem>> GetBasketItemsAsync()
    { 
        Basket? basket = await ContextUser();
        Basket? result = await _basketReadRepository.Table
            .Include(b => b.BasketItems)
            .ThenInclude(bi => bi.Product)
            .FirstOrDefaultAsync(b => b.Id == basket.Id);
        return result.BasketItems.ToList();
    }
    public async Task AddItemToBasketAsync(CreateBasketItemDTO model)
    {
        Basket? basket = await ContextUser();
        if (basket != null)
        {
            BasketItem _basketItem = await _basketItemReadRepository
                .GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == Guid.Parse(model.ProductId));
            if (_basketItem != null)
            {
                _basketItem.Quantity++;
            }
            else
            {
                await _basketItemWriteRepository.AddAsync(new()
                {
                    BasketId = basket.Id,
                    ProductId = Guid.Parse(model.ProductId),
                    Quantity = model.Quantity
                });
            }

            await _basketWriteRepository.SaveAsync();
        }
    }
    public async Task RemoveItemFromBasket(string basketItemId)
    {
        BasketItem basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
        if (basketItem != null)
        {
            _basketItemWriteRepository.Remove(basketItem);
            await _basketItemWriteRepository.SaveAsync();
        }
    }
    public async Task UpdateQuantityAsync(UpdateBasketItemDTO model)
    {
        BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(model.BasketItemId);
        if (basketItem != null)
        {
            basketItem.Quantity = model.Quantity;
            await _basketItemWriteRepository.SaveAsync();
        }
    }
    public Basket? GetUserActiveBasket
    {
        get
        {
            Basket? basket = ContextUser().Result;
            return basket;
        }
    }
    private async Task<Basket?> ContextUser()
    {
        var username =_httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        if (!string.IsNullOrEmpty(username))
        {
            //!!!!!!!!!!!!!!!!!!!!Excellent yemeksepetinde yok bu algoritma  
            AppUser? user = await _userManager.Users.Include(u => u.Baskets)
                .FirstOrDefaultAsync(u => u.UserName == username);

            var _basket = from basket in user.Baskets
                join order in _orderReadRepository.Table on basket.Id equals order.Id into BasketOrder
                from order in BasketOrder.DefaultIfEmpty()
                select new
                {
                    Basket = basket,
                    Order = order
                };

            Basket? targetBasket = null;
            if (_basket.Any(b=> b.Order is null))
                targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
            else
            {
                targetBasket = new();
                user.Baskets.Add(targetBasket);
            }
            await _basketWriteRepository.SaveAsync();
            return targetBasket;
        }

        throw new Exception("Unexpected Error Occur");
    }
}