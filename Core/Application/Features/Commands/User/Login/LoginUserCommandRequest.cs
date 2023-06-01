using MediatR;

namespace Application.Features.Commands.User.Login;

public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
{
    public string UsernameOrEmail { get; set; }

    public string Password { get; set; }
}