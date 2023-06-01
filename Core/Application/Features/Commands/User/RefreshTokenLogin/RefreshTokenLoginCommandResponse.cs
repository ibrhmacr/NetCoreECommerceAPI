using Application.DTOs;

namespace Application.Features.Commands.User.RefreshTokenLogin;

public class RefreshTokenLoginCommandResponse
{
    public Token Token { get; set; }
}