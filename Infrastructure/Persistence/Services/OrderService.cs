using System.Collections;
using Application.Abstractions.Services;
using Application.DTOs.Order;
using Application.Repositories;
using Application.Repositories.CompletedOrder;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

public class OrderService : IOrderService
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly ICompletedOrderWriteRepostiory _completedOrderWriteRepostiory;
    private readonly ICompletedOrderReadRepostiory _completedOrderReadRepostiory;

    public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepostiory completedOrderWriteRepostiory, ICompletedOrderReadRepostiory completedOrderReadRepostiory)
    {
        _orderWriteRepository = orderWriteRepository;
        _orderReadRepository = orderReadRepository;
        _completedOrderWriteRepostiory = completedOrderWriteRepostiory;
        _completedOrderReadRepostiory = completedOrderReadRepostiory;
    }


    public async Task CreateOrder(CreateOrderDTO createOrder)
    {
        var orderCode = (new Random().NextDouble() * 10000000).ToString();
        orderCode = orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);
        
        await _orderWriteRepository.AddAsync(new()
        {
            Address = createOrder.Address,
            Id = Guid.Parse(createOrder.BasketId),
            Description = createOrder.Description,
            OrderCode = orderCode //todo Unique bi deger uretilmediyse tekrardan bi deger uretme operasyonunu gerceklestir
        });
        await _orderWriteRepository.SaveAsync();
    }

    public async Task<ListOrderDTO> GetAllOrdersAsync(int page, int size)
    {
        var query = _orderReadRepository.Table.Include(o => o.Basket)
            .ThenInclude(b => b.User).Include(o => o.Basket)
            .ThenInclude(b => b.BasketItems).ThenInclude(bi => bi.Product);
        
        var data = query.Skip(page*size).Take(size);

        var data2 = from order in data
            join completedOrder in _completedOrderReadRepostiory.Table on order.Id equals completedOrder.Id
                into co
            from _co in co.DefaultIfEmpty()
            select new
            {
                Id = order.Id,
                CreatedDate = order.CreatedDate,
                OrderCode = order.OrderCode,
                Basket = order.Basket,
                Completed = co != null ? true : false
            };

        return new()
        {
            TotalOrderCount = await query.CountAsync(),
            Orders = await data2.Select(o => new
            {
                Id = o.Id,
                CreatedDate = o.CreatedDate,
                OrderCode = o.OrderCode,
                TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                UserName = o.Basket.User.UserName,
                o.Completed
            }).ToListAsync()
        };

    } //!!!!!!!!!!!!

    public async Task<SingleOrderDTO> GetOrderByIdAsync(string id)
    {
        var data = _orderReadRepository.Table
            .Include(o => o.Basket)
            .ThenInclude(b => b.BasketItems)
            .ThenInclude(bi => bi.Product);
        
        var data2 = await (from order in data join completedOrder in _completedOrderReadRepostiory.Table on order.Id equals completedOrder.OrderId
                into co from _co in co.DefaultIfEmpty()
                select new
                {
                    Id = order.Id,
                    CreatedDate = order.CreatedDate,
                    OrderCode = order.OrderCode,
                    Basket = order.Basket,
                    Completed = _co != null ? true : false,
                    Address =order.Address,
                    Description = order.Description
                }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

        return new()
        {
            Id = data2.Id.ToString(),
            BasketItems = data2.Basket.BasketItems.Select(bi => new
            {
                bi.Product.Name,
                bi.Product.Price,
                bi.Quantity
            }),
            Address = data2.Address,
            CreatedDate = data2.CreatedDate,
            Description = data2.Description,
            OrderCode = data2.OrderCode,
            Completed = data2.Completed
        };
    }

    public async Task<(bool, CompletedOrderDTO)> CompleteOrderAsync(string id)
    {
        Order? order = await _orderReadRepository.Table.Include(o => o.Basket).ThenInclude(b => b.User)
            .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
        
        if (order != null)
        {
            await _completedOrderWriteRepostiory.AddAsync(new() { OrderId = Guid.Parse(id) });
            return (await _completedOrderWriteRepostiory.SaveAsync() > 0, new()
            {
                OrderCode = order.OrderCode,
                OrderDate = order.CreatedDate,
                UserName = order.Basket.User.Name,
                UserSurname = order.Basket.User.Surname,
                Email = order.Basket.User.Email
            });
        }
        else
            return (false,null);


    }
} 