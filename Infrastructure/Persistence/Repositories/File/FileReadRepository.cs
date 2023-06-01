using Application.Repositories.File;
using Persistence.Contexts;

namespace Persistence.Repositories.File;

public class FileReadRepository : ReadRepository<Domain.Entities.File> , IFileReadRepository
{
    public FileReadRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}