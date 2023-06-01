using Application.DTOs.Product;
using MediatR;

namespace Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
{
    public string Name { get; set; }

    public string SubCategoryId { get; set; }

    public int UnitsInStock { get; set; }

    public decimal Price { get; set; }
    
}