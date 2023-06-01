using Application.Abstractions.Services;
using Application.DTOs.Product;
using Application.Repositories;
using Application.Repositories.SubCategory;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Persistence.Services;

public class ProductService : IProductService
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IQrCodeService _qrCodeService;
    private readonly ISubCategoryReadRepository _subCategoryReadRepository;

    public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IQrCodeService qrCodeService, ISubCategoryReadRepository subCategoryReadRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _qrCodeService = qrCodeService;
        _subCategoryReadRepository = subCategoryReadRepository;
    }


    //todo Exceptionlari ozellestirme islemleri

    
    public async Task<byte[]> QrCodeToProductAsync(string productId)
    {
        Product product = await _productReadRepository.GetByIdAsync(productId);
        if (product == null)
            throw new Exception("Product not found");

        var plainObject = new
        {
            product.Id,
            product.Name,
            product.Price,
            product.UnitsInStock,
            product.CreatedDate
        };

        string plainText = JsonSerializer.Serialize(plainObject);

        return _qrCodeService.GenerateQrCode(plainText);

    }
    public async Task<bool> CreateProduct(CreateProductDTO createProductDto)
    {
        bool result = await _productWriteRepository.AddAsync(new()
        {
            Name = createProductDto.Name,
            UnitsInStock = createProductDto.UnitsInStock,
            Price = createProductDto.Price,
            SubCategory = await _subCategoryReadRepository.GetByIdAsync(createProductDto.SubCategoryId)
        });

        if (result)
        {
            
            await _productWriteRepository.SaveAsync();
            return true;
        }
        else
            return false;
    }

    public async Task<ListProductDTO> GetAllProduct()
    {
        var products = _productReadRepository.GetALl(false)
            .Include(p => p.ProductImageFiles)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.UnitsInStock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate,
                p.ProductImageFiles

            }).ToListAsync();

        return new()
        {
            Products = await products
        };
    }
}