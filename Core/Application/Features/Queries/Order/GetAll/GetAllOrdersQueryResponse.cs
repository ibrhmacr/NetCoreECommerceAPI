namespace Application.Features.Queries.Order.GetAll;

public class GetAllOrdersQueryResponse
{
    public int TotalOrderCount { get; set; }

    public object Orders { get; set; }
}