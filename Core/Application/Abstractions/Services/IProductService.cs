using Application.DTOs.Product;
using Domain.Entities;

namespace Application.Abstractions.Services;

public interface IProductService
{
    Task<byte[]> QrCodeToProductAsync(string productId);

    Task<bool> CreateProduct(CreateProductDTO createProductDto);
    Task<ListProductDTO> GetAllProduct();
    
    
}