using Application.Repositories.InvoiceFile;
using Persistence.Contexts;

namespace Persistence.Repositories.InvoiceFile;

public class InvoiceFileReadRepository : ReadRepository<Domain.Entities.InvoiceFile>, IInvoiceFileReadRepository
{
    public InvoiceFileReadRepository(ECommerceGradDbContext context) : base(context)
    {
    }
}