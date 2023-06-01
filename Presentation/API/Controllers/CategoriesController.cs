using Application.Features.Commands.Category.Create;
using Application.Features.Queries.Category.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : Controller
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories(GetAllCategoryQueryRequest getAllCategoryQueryRequest)
    {
        GetAllCategoryQueryResponse response = await _mediator.Send(getAllCategoryQueryRequest);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryCommandRequest categoryCommandRequest)
    {
        CreateCategoryCommandResponse response = await _mediator.Send(categoryCommandRequest);
        return Ok(response);
    }
}