using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OrderReadRepository : ReadRepository<Order> , IOrderReadRepository
{
    public OrderReadRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}