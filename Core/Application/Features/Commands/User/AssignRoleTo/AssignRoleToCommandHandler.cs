using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Commands.User.AssignRoleTo;

public class AssignRoleToCommandHandler : IRequestHandler<AssignRoleToCommandRequest, AssignRoleToCommandResponse>
{
    private readonly IUserService _userService;

    public AssignRoleToCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<AssignRoleToCommandResponse> Handle(AssignRoleToCommandRequest request, CancellationToken cancellationToken)
    {
        await _userService.AssignRoleToUserAsync(request.UserId, request.Roles);
        return new();
    }
}