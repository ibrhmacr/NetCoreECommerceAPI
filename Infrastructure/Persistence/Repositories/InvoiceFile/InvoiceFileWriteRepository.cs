using Application.Repositories;
using Application.Repositories.InvoiceFile;
using Persistence.Contexts;

namespace Persistence.Repositories.InvoiceFile;

public class InvoiceFileWriteRepository : WriteRepository<Domain.Entities.InvoiceFile>, IInvoiceFileWriteRepository
{
    public InvoiceFileWriteRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}