using System.Linq.Expressions;
using Application.Repositories.BasketItem;
using Persistence.Contexts;

namespace Persistence.Repositories.BasketItem;

public class BasketItemReadRepository : ReadRepository<Domain.Entities.BasketItem>, IBasketItemReadRepository
{
    public BasketItemReadRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}