using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Commands.User.VerifyResetToken;

public class VerifyResetTokenCommandHandler : IRequestHandler<VerifyResetTokenCommandRequest,VerifyResetTokenCommandResponse>
{
    private readonly IAuthService _authService;

    public VerifyResetTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<VerifyResetTokenCommandResponse> Handle(VerifyResetTokenCommandRequest request, CancellationToken cancellationToken)
    {
        bool state = await _authService.VerifyResetTokenAsync(request.ResetToken, request.UserId);
        return new()
        {
            State = state
        };
    }
}