using Domain.Entities.Common;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class Endpoint : BaseEntity
{
    public Endpoint()
    {
        Roles = new HashSet<AppRole>();
    }
    public string Actiontype { get; set; }

    public string HttpType { get; set; }

    public string Definition { get; set; }

    public string Code { get; set; }
    
    public Menu Menu { get; set; }

    public ICollection<AppRole> Roles { get; set; }
}