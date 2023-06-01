using Application.DTOs.Order;
using Application.DTOs.Product;
using Application.DTOs.SubCategory;

namespace Application.Abstractions.Services;

public interface ISubCategoryService
{
    Task<bool> CreateSubCategory(CreateSubCategoryDTO createSubCategory);

    Task<object> GetSubCategoryWithProducts(string subCategoryId);
}