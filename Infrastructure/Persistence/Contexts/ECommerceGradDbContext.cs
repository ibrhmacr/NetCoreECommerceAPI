using Domain.Entities;
using Domain.Entities.Common;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = Domain.Entities.File;

namespace Persistence.Contexts;
public class ECommerceGradDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public ECommerceGradDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    
    public DbSet<Order> Orders { get; set; }

    public DbSet<File> Files { get; set; }

    public DbSet<ProductImageFile> ProductImageFiles { get; set; }

    public DbSet<InvoiceFile> InvoiceFiles { get; set; }

    public DbSet<Basket> Baskets { get; set; }

    public DbSet<BasketItem> BasketItems { get; set; }

    public DbSet<CompletedOrder> CompletedOrders { get; set; }
    
    public DbSet<Menu> Menus { get; set; }
    
    public DbSet<Endpoint> Endpoints { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<SubCategory> SubCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Order>().HasKey(b => b.Id);

        builder.Entity<Order>()
            .HasIndex(o => o.OrderCode)
            .IsUnique();
        
        builder.Entity<Basket>().HasOne(b => b.Order).WithOne(o => o.Basket)
            .HasForeignKey<Order>(b => b.Id);

        builder.Entity<Order>().HasOne(o => o.CompletedOrder).WithOne(co => co.Order)
            .HasForeignKey<CompletedOrder>(co => co.OrderId);
        
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var datas = ChangeTracker.Entries<BaseEntity>();

        foreach (var data in datas)
        {
            _ = data.State switch // discard islemi, herhangi bir atama yapmasinin gereksiz oldugunu belirtiyoruz.
            {
                EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                _ => DateTime.UtcNow
            };
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}