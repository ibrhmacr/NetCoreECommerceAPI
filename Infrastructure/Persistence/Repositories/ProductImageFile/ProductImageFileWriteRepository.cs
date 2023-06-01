using Application.Repositories;
using Application.Repositories.ProductImageFile;
using Persistence.Contexts;

namespace Persistence.Repositories.ProductImageFile;

public class ProductImageFileWriteRepository : WriteRepository<Domain.Entities.ProductImageFile>, IProductImageFileWriteRepository
{
    public ProductImageFileWriteRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}