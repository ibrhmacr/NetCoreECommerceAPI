using MediatR;


namespace Application.Features.Commands.Product.Remove;

public class RemoveProductCommandRequest : IRequest<RemoveProductCommandResponse>
{
    public string Id { get; set; }
}