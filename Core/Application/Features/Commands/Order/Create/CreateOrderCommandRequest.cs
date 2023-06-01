using MediatR;

namespace Application.Features.Commands.Order.Create;

public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResponse>
{
    public string Description { get; set; }
    
    public string Address { get; set; }
}