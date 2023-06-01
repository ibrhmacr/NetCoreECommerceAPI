using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProductWriteRepository : WriteRepository<Product> , IProductWriteRepository
{
    public ProductWriteRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}