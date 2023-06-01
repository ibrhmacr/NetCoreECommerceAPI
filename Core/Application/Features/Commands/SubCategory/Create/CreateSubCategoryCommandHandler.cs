using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Commands.SubCategory.Create;

public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommandRequest, CreateSubCategoryCommandResponse>
{
    private readonly ISubCategoryService _subCategoryService;

    public CreateSubCategoryCommandHandler(ISubCategoryService subCategoryService)
    {
        _subCategoryService = subCategoryService;
    }

    public async Task<CreateSubCategoryCommandResponse> Handle(CreateSubCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _subCategoryService.CreateSubCategory(new()
        {
            Name = request.Name,
            CategoryId = request.CategoryId
        });
        if (result)
            return new();
        else
            throw new Exception("SubCategory Add Fail");

    }
}