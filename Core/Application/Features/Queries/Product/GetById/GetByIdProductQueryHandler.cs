using Application.Repositories;
using MediatR;

namespace Application.Features.Queries.Product.GetById;

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id, false);
        return new()
        {
            Name = product.Name,
            Price = product.Price,
            UnitsInStock = product.UnitsInStock
        };
    }
}