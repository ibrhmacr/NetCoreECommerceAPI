using Application.CustomAttribute;
using Application.Enums;
using Application.Features.Commands.User.AssignRoleTo;
using Application.Features.Commands.User.Create;
using Application.Features.Commands.User.UpdatePassword;
using Application.Features.Queries.AppUser.GetAllUsers;
using Application.Features.Queries.AppUser.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]

public class UsersController : Controller
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
    {
        CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
        return Ok(response);
    }

    [HttpPost("update-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
    {
        UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
        return Ok(response);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(ActionType = ActionType.Reading,Definition = "Get All Users", Menu = "Users")]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
    {
        GetAllUsersQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
        return Ok(response);
    } 
    
    [HttpGet("get-roles-to-user/{userId}")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(ActionType = ActionType.Reading,Definition = "Get Roles To User", Menu = "Users")]
    public async Task<IActionResult> GetRolesUser([FromRoute] GetRolesUserQueryRequest getRolesUserQueryRequest)
    {
        GetRolesUserQueryResponse response = await _mediator.Send(getRolesUserQueryRequest);
        return Ok(response); 
    } 
    
    [HttpPost("assign-role-to-user")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(ActionType = ActionType.Reading,Definition = "Assign Role To User", Menu = "Users")]
    public async Task<IActionResult> AssignRoleToUser(AssignRoleToCommandRequest assignRoleToCommandRequest)
    {
        AssignRoleToCommandResponse response = await _mediator.Send(assignRoleToCommandRequest);
        return Ok(response);
    }
}