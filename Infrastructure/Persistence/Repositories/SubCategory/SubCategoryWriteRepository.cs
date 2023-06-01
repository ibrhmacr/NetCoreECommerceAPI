using Application.Repositories.SubCategory;
using Persistence.Contexts;

namespace Persistence.Repositories.SubCategory;

public class SubCategoryWriteRepository : WriteRepository<Domain.Entities.SubCategory>, ISubCategoryWriteRepository
{
    public SubCategoryWriteRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}