using MediatR;

namespace Application.Features.Commands.User.PasswordReset;

public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
{
    public string Email { get; set; }
}