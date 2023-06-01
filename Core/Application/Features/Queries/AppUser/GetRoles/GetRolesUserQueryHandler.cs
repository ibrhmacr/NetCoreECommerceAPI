using Application.Abstractions.Services;
using Application.Features.Queries.AppUser.GetAllUsers;
using MediatR;

namespace Application.Features.Queries.AppUser.GetRoles;

public class GetRolesUserQueryHandler : IRequestHandler<GetRolesUserQueryRequest, GetRolesUserQueryResponse>
{
    private readonly IUserService _userService;

    public GetRolesUserQueryHandler(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<GetRolesUserQueryResponse> Handle(GetRolesUserQueryRequest request, CancellationToken cancellationToken)
    {
        var userRoles = await _userService.GetRolesUserAsync(request.UserId);
        return new()
        {
            UserRoles = userRoles
        };
    }
}