using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Queries.Order.GetById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest,GetOrderByIdQueryResponse>
{
    private readonly IOrderService _orderService;

    public GetOrderByIdQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await _orderService.GetOrderByIdAsync(request.Id);

        return new()
        {
            Id = data.Id,
            Address = data.Address,
            BasketItems = data.BasketItems,
            CreatedDate = data.CreatedDate,
            Description = data.Description,
            OrderCode = data.OrderCode,
            Completed = data.Completed
        };
    }
}