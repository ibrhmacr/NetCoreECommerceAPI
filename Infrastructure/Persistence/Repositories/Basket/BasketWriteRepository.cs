using Application.Repositories.Basket;
using Persistence.Contexts;

namespace Persistence.Repositories.Basket;

public class BasketWriteRepository : WriteRepository<Domain.Entities.Basket>, IBasketWriteRepository
{
    public BasketWriteRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}