using Application.Repositories.Basket;
using Persistence.Contexts;

namespace Persistence.Repositories.Basket;

public class BasketReadRepository : ReadRepository<Domain.Entities.Basket> , IBasketReadRepository
{
    public BasketReadRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}