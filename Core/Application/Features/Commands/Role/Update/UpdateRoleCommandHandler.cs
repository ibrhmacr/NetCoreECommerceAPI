using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Commands.Role.Update;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
{
    private readonly IRoleService _roleService;

    public UpdateRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _roleService.UpdateRole(request.Id, request.Name);
        return new UpdateRoleCommandResponse()
        {
            Succeeded = result
        };
    }
}