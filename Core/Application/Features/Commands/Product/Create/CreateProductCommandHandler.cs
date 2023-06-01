using Application.Abstractions.Hubs;
using Application.Abstractions.Services;
using Application.Exceptions;
using Application.Features.Commands.Product.CreateProduct;
using Application.Repositories;
using MediatR;


namespace Application.Features.Commands.Product.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest,CreateProductCommandResponse>
{
    private readonly IProductService _productService;
    private readonly IProductHubService _productHubService;

    public CreateProductCommandHandler(IProductService productService, IProductHubService productHubService)
    {
        _productService = productService;
        _productHubService = productHubService;
    }


    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _productService.CreateProduct(new()
        {
            Name = request.Name,
            Price = request.Price,
            SubCategoryId = request.SubCategoryId,
            UnitsInStock = request.UnitsInStock
        });
        if (result)
        {
            await _productHubService.ProductAddedMessageAsync($"{request.Name} isminde urun eklenmistir");
            return new();
        }
        else
            throw new AddProductFailedException();
        
        

    }
}