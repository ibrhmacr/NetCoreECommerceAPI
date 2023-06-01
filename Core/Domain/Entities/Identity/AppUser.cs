using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class AppUser : IdentityUser<string>
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenEndDate { get; set; }

    public ICollection<Basket> Baskets { get; set; }
}