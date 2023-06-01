using MediatR;

namespace Application.Features.Commands.Product.Update;

public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
{
    public string Id { get; set; }

    public string Name { get; set; }

    public int UnitsInStock { get; set; }

    public decimal Price { get; set; }
    
}