namespace Application.DTOs.Product;

public class CreateProductDTO
{
    public string Name { get; set; }

    public string SubCategoryId { get; set; }

    public int UnitsInStock { get; set; }

    public decimal Price { get; set; }
    
    
}