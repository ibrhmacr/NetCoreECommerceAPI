using Application.DTOs.Category;

namespace Application.Abstractions.Services;

public interface ICategoryService
{
    ListCategoryDTO GetCategories();

    Task<bool> CrateCategory(string name);
}