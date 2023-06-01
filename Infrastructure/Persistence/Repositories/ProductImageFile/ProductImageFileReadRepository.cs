using Application.Repositories;
using Application.Repositories.ProductImageFile;
using Persistence.Contexts;

namespace Persistence.Repositories.ProductImageFile;

public class ProductImageFileReadRepository : ReadRepository<Domain.Entities.ProductImageFile>, IProductImageFileReadRepository
{
    public ProductImageFileReadRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}