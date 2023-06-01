using Application.Repositories.Category;
using Persistence.Contexts;

namespace Persistence.Repositories.Category;

public class CategoryReadRepository : ReadRepository<Domain.Entities.Category>, ICategoryReadRepository
{
    public CategoryReadRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}