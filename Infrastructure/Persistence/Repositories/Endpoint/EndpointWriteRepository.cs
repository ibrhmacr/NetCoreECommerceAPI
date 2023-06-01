using Application.Repositories.Endpoint;
using Persistence.Contexts;

namespace Persistence.Repositories.Endpoint;

public class EndpointWriteRepository : WriteRepository<Domain.Entities.Endpoint>, IEndpointWriteRepository
{
    public EndpointWriteRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}