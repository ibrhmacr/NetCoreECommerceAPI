using Application.Abstractions.Services;
using Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.Product.GetAll;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
{
    private readonly IProductService _productService;

    public GetAllProductQueryHandler(IProductService productService)
    {
        _productService = productService;
    }


    public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
        //var totalProductCount = _productReadRepository.GetALl(false).Count();

        var products = await _productService.GetAllProduct();
        return new GetAllProductQueryResponse()
        {
            Products = products
        };

    }                                                               
}