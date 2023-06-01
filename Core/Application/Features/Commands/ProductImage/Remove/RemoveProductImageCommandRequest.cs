using MediatR;

namespace Application.Features.Commands.ProductImage.Remove;

public class RemoveProductImageCommandRequest : IRequest<RemoveProductImageCommandResponse>
{
    public string Id { get; set; }

    public string? ImageId { get; set; }


}