using Application.Abstractions.Services;
using Application.Abstractions.Token;
using Application.DTOs;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.User.GoogleLogin;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
{
    private readonly IAuthService _authService;

    public GoogleLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }
    
    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var token = await _authService.GoogleLoginAsync(request.IdToken, 900);
        return new()
        {
            Token = token
        };
    }
}