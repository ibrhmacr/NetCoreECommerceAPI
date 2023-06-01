using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Commands.Role.Create;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
{
    private readonly IRoleService _roleService;

    public CreateRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _roleService.CreateRole(request.Name);

        return new()
        {
            Succeeded = result
        };
    }
}