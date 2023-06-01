using Application.Repositories.CompletedOrder;
using Persistence.Contexts;

namespace Persistence.Repositories.CompletedOrder;

public class CompletedOrderWriteRepository : WriteRepository<Domain.Entities.CompletedOrder>, ICompletedOrderWriteRepostiory
{
    public CompletedOrderWriteRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}