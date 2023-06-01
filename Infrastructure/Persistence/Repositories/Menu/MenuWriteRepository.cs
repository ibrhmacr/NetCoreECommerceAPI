using Application.Repositories.Menu;
using Persistence.Contexts;

namespace Persistence.Repositories.Menu;

public class MenuWriteRepository : WriteRepository<Domain.Entities.Menu>, IMenuWriteRepository
{
    public MenuWriteRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}