using Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationEndpointsController : Controller
{
    private readonly IMediator _mediator;

    public AuthorizationEndpointsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("get-roles-to-endpoint")]
    public async Task<IActionResult> GetRolesToEndpoint(GetRolesToEndpointQueryRequest getRolesToEndpointQueryRequest)
    {
        GetRolesToEndpointQueryResponse response = await _mediator.Send(getRolesToEndpointQueryRequest);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommandRequest assignRoleEndpointCommandRequest)
    {
        assignRoleEndpointCommandRequest.Type = typeof(Program);
        AssignRoleEndpointCommandResponse response = await _mediator.Send(assignRoleEndpointCommandRequest);
        return Ok(response);
    }
}