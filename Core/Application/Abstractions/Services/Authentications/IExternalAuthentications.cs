namespace Application.Abstractions.Services.Authentications;

public interface IExternalAuthentications
{
    Task<DTOs.Token> GoogleLoginAsync(string idToken, int accessTokenLifetime);
}