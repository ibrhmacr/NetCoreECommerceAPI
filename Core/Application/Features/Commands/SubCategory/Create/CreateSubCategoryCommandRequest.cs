using MediatR;

namespace Application.Features.Commands.SubCategory.Create;

public class CreateSubCategoryCommandRequest : IRequest<CreateSubCategoryCommandResponse>
{
    public string Name { get; set; }

    public string CategoryId { get; set; }
}