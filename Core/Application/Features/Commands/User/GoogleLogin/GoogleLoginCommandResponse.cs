using Application.DTOs;

namespace Application.Features.Commands.User.GoogleLogin;

public class GoogleLoginCommandResponse
{
    public Token Token { get; set; }
}