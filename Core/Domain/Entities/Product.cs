using System.Security.Cryptography;
using Domain.Entities.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public int UnitsInStock { get; set; }
    public decimal Price { get; set; }
    
    public SubCategory SubCategory { get; set; }
    public ICollection<ProductImageFile> ProductImageFiles { get; set; }

    public ICollection<BasketItem> BasketItems { get; set; }
    
    // public ICollection<Order> Orders { get; set; }
            
}