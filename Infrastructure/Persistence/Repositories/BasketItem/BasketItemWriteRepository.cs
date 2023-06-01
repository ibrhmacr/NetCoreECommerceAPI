using Application.Repositories.BasketItem;
using Persistence.Contexts;

namespace Persistence.Repositories.BasketItem;

public class BasketItemWriteRepository : WriteRepository<Domain.Entities.BasketItem> , IBasketItemWriteRepository
{
    public BasketItemWriteRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}