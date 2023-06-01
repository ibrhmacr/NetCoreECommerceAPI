using Application.Abstractions.Services;
using Application.DTOs;
using MediatR;

namespace Application.Features.Commands.User.RefreshTokenLogin;

public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
    {
        Token token = await _authService.RefrehTokenLoginAsync(request.RefreshToken);
        return new()
        {
            Token = token
        };
    }
}