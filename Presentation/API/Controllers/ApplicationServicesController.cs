using Application.Abstractions.Services.Configurations;
using Application.CustomAttribute;
using Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Admin")]

public class ApplicationServicesController : Controller
{
    private readonly IApplicationService _applicationService;

    public ApplicationServicesController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpGet]
    [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Authorize Definition Endpoint", Menu = "Application Services")]
    public IActionResult GetAuthorizeDefinitionEndpoints()
    {
        var datas = _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
        return Ok(datas);
    }
}