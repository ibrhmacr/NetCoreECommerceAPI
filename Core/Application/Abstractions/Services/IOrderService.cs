using Application.DTOs.Order;
using Domain.Entities;

namespace Application.Abstractions.Services;

public interface IOrderService
{
    Task CreateOrder(CreateOrderDTO createOrder);

    Task<ListOrderDTO> GetAllOrdersAsync(int page, int size);

    Task<SingleOrderDTO> GetOrderByIdAsync(string id);

    Task<(bool,CompletedOrderDTO)> CompleteOrderAsync(string id);


}