using Application.Abstractions.Services.Authentications;

namespace Application.Abstractions.Services;

public interface IAuthService: IInternalAuthentication, IExternalAuthentications
{
    Task PasswordResetAsync(string email);
    Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
}