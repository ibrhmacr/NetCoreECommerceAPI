using Application.Abstractions.Services;
using Application.Abstractions.Services.Authentications;
using Application.Repositories;
using Application.Repositories.Basket;
using Application.Repositories.BasketItem;
using Application.Repositories.Category;
using Application.Repositories.CompletedOrder;
using Application.Repositories.Endpoint;
using Application.Repositories.File;
using Application.Repositories.InvoiceFile;
using Application.Repositories.Menu;
using Application.Repositories.ProductImageFile;
using Application.Repositories.SubCategory;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Persistence.Repositories.Basket;
using Persistence.Repositories.BasketItem;
using Persistence.Repositories.Category;
using Persistence.Repositories.CompletedOrder;
using Persistence.Repositories.Endpoint;
using Persistence.Repositories.File;
using Persistence.Repositories.InvoiceFile;
using Persistence.Repositories.Menu;
using Persistence.Repositories.ProductImageFile;
using Persistence.Repositories.SubCategory;
using Persistence.Services;

namespace Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ECommerceGradDbContext>(options =>
            options.UseNpgsql(Configuration.ConnectionString));

        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<ECommerceGradDbContext>()
            .AddDefaultTokenProviders() ;
        
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        services.AddScoped<IFileReadRepository, FileReadRepository>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();
        
        services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

        services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
        
        services.AddScoped<IBasketReadRepository, BasketReadRepository>();
        services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

        services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
        services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
        
        services.AddScoped<ICompletedOrderReadRepostiory, CompletedOrderReadRepository>();
        services.AddScoped<ICompletedOrderWriteRepostiory, CompletedOrderWriteRepository>();
        
        services.AddScoped<IMenuReadRepository, MenuReadRepository>();
        services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();

        services.AddScoped<IEndpointReadRepostiory, EndpointReadRepository>();
        services.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();

        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

        services.AddScoped<ISubCategoryReadRepository, SubCategoryReadRepository>();
        services.AddScoped<ISubCategoryWriteRepository, SubCategoryWriteRepository>();
        
        services.AddScoped<IUserService, UserService>();
        
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IOrderService, OrderService>();
        
        services.AddScoped<IBasketService, BasketService>();
        
        services.AddScoped<IRoleService, RoleService>();

        services.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();

        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<ISubCategoryService, SubCategoryService>();




    }
}