using MediatR;

namespace Application.Features.Queries.ProductImage.GetProductImages;

public class GetProductImagesQueryRequest : IRequest<List<GetProductImagesQueryResponse>>
{
    public string Id { get; set; }
}