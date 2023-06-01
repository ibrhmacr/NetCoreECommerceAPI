using Application.Abstractions.Hubs;
using Microsoft.Extensions.DependencyInjection;
using SignalR.HubServices;

namespace SignalR;

public static class ServiceRegistration
{
    public static void AddSignalRServices(this IServiceCollection collection)
    {
        collection.AddTransient<IProductHubService, ProductHubService>();
        collection.AddTransient<IOrderHubService, OrderHubService>();
        collection.AddSignalR(); // Bu Sayede SignalR kutuphanesindeki butun fonksiyonlari IoC ye tanitmis oluyoruz.
    }
}