using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persistence.Contexts;

namespace Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceGradDbContext>
{
    public ECommerceGradDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ECommerceGradDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);

        return new ECommerceGradDbContext(dbContextOptionsBuilder.Options);
    }
}