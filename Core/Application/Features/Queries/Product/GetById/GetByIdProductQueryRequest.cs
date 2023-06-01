using MediatR;

namespace Application.Features.Queries.Product.GetById;

public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
{
    public string Id { get; set; }
}