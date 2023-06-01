using Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Commands.Product.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }
    
    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
        product.Name = request.Name;
        product.UnitsInStock = request.UnitsInStock;
        product.Price = request.Price;
        await _productWriteRepository.SaveAsync();
        return new();

    }
}