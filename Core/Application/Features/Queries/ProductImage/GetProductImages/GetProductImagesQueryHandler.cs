using Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.ProductImage.GetProductImages;

public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest,List<GetProductImagesQueryResponse>>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetProductImagesQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

        return product?.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
        {
            Path = p.Path, //Todo Pathin verilmesi gerekiyor!!!
            FileName = p.FileName,
            Id = p.Id
        }).ToList();
    }
}