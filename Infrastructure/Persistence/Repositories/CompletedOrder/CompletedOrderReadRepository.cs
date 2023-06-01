using Application.Repositories.CompletedOrder;
using Persistence.Contexts;

namespace Persistence.Repositories.CompletedOrder;

public class CompletedOrderReadRepository : ReadRepository<Domain.Entities.CompletedOrder> , ICompletedOrderReadRepostiory
{
    public CompletedOrderReadRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}