using Application.Abstractions.Services;
using Application.Features.Queries.Role.GetRoles;
using MediatR;

namespace Application.Features.Queries.Role.GetRoleById;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse>
{
    private readonly IRoleService _roleService;

    public GetRoleByIdQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }


    public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await _roleService.GetRoleById(request.Id);
        return new()
        {
            Id = data.id,
            Name = data.name
        };
    }
}