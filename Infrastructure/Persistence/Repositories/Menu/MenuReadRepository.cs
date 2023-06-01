using Application.Repositories.Menu;
using Persistence.Contexts;

namespace Persistence.Repositories.Menu;

public class MenuReadRepository : ReadRepository<Domain.Entities.Menu> , IMenuReadRepository
{
    public MenuReadRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}