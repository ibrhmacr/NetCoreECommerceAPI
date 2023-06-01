using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Queries.Category.GetAll;

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, GetAllCategoryQueryResponse>
{
    private readonly ICategoryService _categoryService;

    public GetAllCategoryQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<GetAllCategoryQueryResponse> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
    {
        var categories = _categoryService.GetCategories();

        return new()
        {
            Categories = categories
        };

    }
}