using Domain.Entities.Identity;

namespace Application.Abstractions.Token;

public interface ITokenHandler
{
    DTOs.Token CreateAccessToken(int second, AppUser user);

    string CreateRefreshToken();
}