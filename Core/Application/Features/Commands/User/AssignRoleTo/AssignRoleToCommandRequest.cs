using MediatR;

namespace Application.Features.Commands.User.AssignRoleTo;

public class AssignRoleToCommandRequest : IRequest<AssignRoleToCommandResponse>
{
    public string UserId { get; set; }

    public string[] Roles { get; set; }
}