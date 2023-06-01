using Application.Abstractions.Services;
using Application.DTOs.User;
using Application.Exceptions;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        if (!request.Password.Equals(request.PasswordConfirm))
        {
            throw new PasswordChangeFailedException();
        }
        CreateUserResponseDTO response = await _userService.CreateAsync(new()
        {
            Name = request.Name,
            Surname = request.Surname,
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
            PasswordConfirm = request.PasswordConfirm
        });
        return new()
        {
            Message = response.Message,
            Succeded = response.Succeded
        } ;
    }
}
