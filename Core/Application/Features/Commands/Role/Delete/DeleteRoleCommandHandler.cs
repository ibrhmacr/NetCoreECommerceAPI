using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Commands.Role.Delete;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
{
    private readonly IRoleService _roleService;

    public DeleteRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }


    public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _roleService.DeleteRole(request.Id);

        return new()
        {
            Succeeded = result
        };
    }
}