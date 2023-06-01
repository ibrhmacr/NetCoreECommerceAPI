using Application.Repositories;
using MediatR;

namespace Application.Features.Commands.Product.Remove;

public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest,RemoveProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;

    public RemoveProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productWriteRepository.RemoveAsync(request.Id);
        await _productWriteRepository.SaveAsync();
        return new();
    }
}