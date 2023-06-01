using Application.DTOs.Product;

namespace Application.Features.Queries.Product.GetAll;

public class GetAllProductQueryResponse
{
    //public int TotalProductCount { get; set; }

    public ListProductDTO Products { get; set; }
}