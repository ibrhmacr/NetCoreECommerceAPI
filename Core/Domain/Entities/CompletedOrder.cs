using Domain.Entities.Common;

namespace Domain.Entities;

public class CompletedOrder : BaseEntity
{
    public Guid OrderId { get; set; } // Bu id ye ait order tamamlanmistir bilgisini tutmak yeterli olucaktir

    public Order Order { get; set; }
}