using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Queries.Role.GetRoles;

public class GetRolesQueryHandler :IRequestHandler<GetRolesQueryRequest,GetRolesQueryResponse>
{
    private readonly IRoleService _roleService;

    public GetRolesQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<GetRolesQueryResponse> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
    {
        var (data, count) = _roleService.GetAllRoles(request.Page, request.Size);
        
        return new()
        {
            Datas = data,
            TotalRoleCount = count
        };
    }
}