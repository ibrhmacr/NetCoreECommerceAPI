using Application.Repositories.File;
using Persistence.Contexts;

namespace Persistence.Repositories.File;

public class FileWriteRepository : WriteRepository<Domain.Entities.File> , IFileWriteRepository
{
    public FileWriteRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}