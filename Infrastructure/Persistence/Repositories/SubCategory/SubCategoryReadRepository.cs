using Application.Repositories.SubCategory;
using Persistence.Contexts;

namespace Persistence.Repositories.SubCategory;

public class SubCategoryReadRepository : ReadRepository<Domain.Entities.SubCategory>, ISubCategoryReadRepository
{
    public SubCategoryReadRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}