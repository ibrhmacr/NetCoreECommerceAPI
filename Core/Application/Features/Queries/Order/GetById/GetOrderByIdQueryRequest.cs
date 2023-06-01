using MediatR;

namespace Application.Features.Queries.Order.GetById;

public class GetOrderByIdQueryRequest : IRequest<GetOrderByIdQueryResponse>
{
    public string Id { get; set; }
}