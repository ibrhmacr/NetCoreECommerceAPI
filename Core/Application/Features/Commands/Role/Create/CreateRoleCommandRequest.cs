using MediatR;

namespace Application.Features.Commands.Role.Create;

public class CreateRoleCommandRequest : IRequest<CreateRoleCommandResponse>
{
    public string Name { get; set; }
}