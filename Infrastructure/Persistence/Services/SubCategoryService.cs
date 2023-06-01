using Application.Abstractions.Services;
using Application.DTOs.Product;
using Application.DTOs.SubCategory;
using Application.Repositories;
using Application.Repositories.SubCategory;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

public class SubCategoryService : ISubCategoryService
{
    private readonly ISubCategoryWriteRepository _subCategoryWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly ISubCategoryReadRepository _subCategoryReadRepository;

    public SubCategoryService(ISubCategoryWriteRepository subCategoryWriteRepository, IProductReadRepository productReadRepository, ISubCategoryReadRepository subCategoryReadRepository)
    {
        _subCategoryWriteRepository = subCategoryWriteRepository;
        _productReadRepository = productReadRepository;
        _subCategoryReadRepository = subCategoryReadRepository;
    }
    public async Task<bool> CreateSubCategory(CreateSubCategoryDTO createSubCategory)
    {
        
        var result = await _subCategoryWriteRepository.AddAsync(new()
        {
            Name = createSubCategory.Name,
            CategoryId = Guid.Parse(createSubCategory.CategoryId)
        });
        if (result)
        {
            await _subCategoryWriteRepository.SaveAsync();
            return true;
        }
        else
            throw new Exception("SubCategory Add Fail");
    }

    public async Task<object> GetSubCategoryWithProducts(string subCategoryId)
    {
        var products = await _productReadRepository.GetALl().Where(p => p.SubCategory.Id == Guid.Parse(subCategoryId)).ToListAsync();
        return products;
    }
}