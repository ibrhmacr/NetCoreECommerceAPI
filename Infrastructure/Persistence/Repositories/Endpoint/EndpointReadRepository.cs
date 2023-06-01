using Application.Repositories.Endpoint;
using Persistence.Contexts;

namespace Persistence.Repositories.Endpoint;

public class EndpointReadRepository : ReadRepository<Domain.Entities.Endpoint>, IEndpointReadRepostiory
{
    public EndpointReadRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}