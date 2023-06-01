using MediatR;

namespace Application.Features.Commands.Role.Update;

public class UpdateRoleCommandRequest : IRequest<UpdateRoleCommandResponse>
{
    public string Id { get; set; }

    public string Name { get; set; }
}