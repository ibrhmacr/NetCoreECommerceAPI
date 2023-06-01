using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Commands.Category.Create;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, CreateCategoryCommandResponse>
{
    private readonly ICategoryService _categoryService;

    public CreateCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryService.CrateCategory(request.Name);
        
        if (result)
            return new();
        else
            throw new("Category add fail");
        
        
    }
}