using Application.Abstractions.Services;
using Application.Abstractions.Services.Configurations;
using Application.Abstractions.Storage;
using Application.Abstractions.Token;
using Infrastructure.Enums;
using Infrastructure.Services.Configurations;
using Infrastructure.Services.Mail;
using Infrastructure.Services.QrCode;
using Infrastructure.Services.Storage;
using Infrastructure.Services.Storage.AzureStorage;
using Infrastructure.Services.Storage.LocalStorage;
using Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStorageService, StorageService>();
        serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
        serviceCollection.AddScoped<IMailService, MailService>();
        serviceCollection.AddScoped<IApplicationService, ApplicationService>();
        serviceCollection.AddScoped<IQrCodeService, QrCodeService>();

    }

    public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
    {
        serviceCollection.AddScoped<IStorage, T>();
    }
    
    //Tamamen keyfi yapilmis bir uygulamadir buradaki switch case yapisini kaldirsakta devam ederiz.
    public static void AddStorage<T>(this IServiceCollection serviceCollection, StorageType storageType) where T : Storage, IStorage
    {
        switch (storageType)
        {
            case StorageType.Local:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
            case StorageType.Azure:
                serviceCollection.AddScoped<IStorage, AzureStorage>();
                break;
            case StorageType.Aws:
                //serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
            default:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
                
        }
    }
}