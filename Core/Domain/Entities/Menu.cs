using Domain.Entities.Common;

namespace Domain.Entities;

public class Menu : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Endpoint> Endpoints { get; set; } //NavigationProperty
}