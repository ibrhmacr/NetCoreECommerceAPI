namespace Application.DTOs.Order;

public class ListOrderDTO
{
    public int TotalOrderCount { get; set; }

    public object Orders { get; set; }
}