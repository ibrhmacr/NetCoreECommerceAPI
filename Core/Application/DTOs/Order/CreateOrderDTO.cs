namespace Application.DTOs.Order;

public class CreateOrderDTO
{
    public string? BasketId { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }
}