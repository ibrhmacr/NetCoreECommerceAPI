using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;

public class AssignRoleEndpointCommandHandler : IRequestHandler<AssignRoleEndpointCommandRequest, AssignRoleEndpointCommandResponse>
{
    private readonly IAuthorizationEndpointService _authorizationEndpointService;

    public AssignRoleEndpointCommandHandler(IAuthorizationEndpointService authorizationEndpointService)
    {
        _authorizationEndpointService = authorizationEndpointService;
    }

    public async Task<AssignRoleEndpointCommandResponse> Handle(AssignRoleEndpointCommandRequest request, CancellationToken cancellationToken)
    {
        await _authorizationEndpointService.AssignRoleEndpointAsync(request.Roles, request.Menu, request.EndpointCode,
            request.Type);

        return new()
        {

        };
    }
}