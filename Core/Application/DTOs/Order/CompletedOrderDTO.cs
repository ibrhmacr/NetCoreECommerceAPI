using System.Runtime.InteropServices.JavaScript;

namespace Application.DTOs.Order;

public class CompletedOrderDTO
{
    public string OrderCode { get; set; }

    public DateTime OrderDate { get; set; }

    public string UserName { get; set; }

    public string UserSurname { get; set; }

    public string Email { get; set; }
}