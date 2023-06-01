namespace Domain.Entities;

public class ProductImageFile : File
{
    public bool Showcase { get; set; } //Vitrin gorseli ozelligi icin dilersek kaldirqabiliriz.
    public ICollection<Product> Products { get; set; }
}