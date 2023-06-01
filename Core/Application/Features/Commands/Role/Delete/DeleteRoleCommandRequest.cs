using MediatR;

namespace Application.Features.Commands.Role.Delete;

public class DeleteRoleCommandRequest : IRequest<DeleteRoleCommandResponse>
{
    public string Id { get; set; }
}