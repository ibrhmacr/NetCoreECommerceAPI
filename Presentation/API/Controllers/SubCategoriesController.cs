using Application.Abstractions.Services;
using Application.Features.Commands.SubCategory.Create;
using Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class SubCategoriesController : Controller
{
    private readonly IMediator _mediator;
    private readonly ISubCategoryService _subCategoryService;
    private readonly IProductReadRepository _productReadRepository;

    public SubCategoriesController(IMediator mediator, ISubCategoryService subCategoryService, IProductReadRepository productReadRepository)
    {
        _mediator = mediator;
        _subCategoryService = subCategoryService;
        _productReadRepository = productReadRepository;
    }


    [HttpPost]
    public async Task<IActionResult> CreateSubCategory(CreateSubCategoryCommandRequest createSubCategoryCommandRequest)
    {
        CreateSubCategoryCommandResponse response = await _mediator.Send(createSubCategoryCommandRequest);
        return Ok(response);
    }

    [HttpGet("{subCategoryId}")]
    public async Task<IActionResult> GetSubCategoryWithProducts([FromRoute]string subCategoryId)
    {
        var result = await _subCategoryService.GetSubCategoryWithProducts(subCategoryId);
        return Ok(result);
    }
}