using Application.Abstractions.Services;
using Application.DTOs.Category;
using Application.Repositories.Category;

namespace Persistence.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryReadRepository _categoryReadRepository;
    private readonly ICategoryWriteRepository _categoryWriteRepository;

    public CategoryService(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository)
    {
        _categoryReadRepository = categoryReadRepository;
        _categoryWriteRepository = categoryWriteRepository;
    }


    public ListCategoryDTO GetCategories()
    {
        var categories = _categoryReadRepository.GetALl();

        return new()
        {
            Categories = categories
        };
    }

    public async Task<bool> CrateCategory(string name)
    {
        bool result = await _categoryWriteRepository.AddAsync(new()
        {
            Name = name,
        });

        if (result)
        {
            await _categoryWriteRepository.SaveAsync();
            return result;
        }
        else
            throw new("CreateCategory fail");
    }
}