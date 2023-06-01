using MediatR;

namespace Application.Features.Commands.Category.Create;

public class CreateCategoryCommandRequest : IRequest<CreateCategoryCommandResponse>
{
    public string Name { get; set; }
    
}