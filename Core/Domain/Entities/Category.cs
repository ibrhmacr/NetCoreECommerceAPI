using Domain.Entities.Common;

namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    
    public ICollection<SubCategory> SubCategories { get; set; }
}