using System.Net;
using Application.Abstractions.Services;
using Application.Consts;
using Application.CustomAttribute;
using Application.Enums;
using Application.Features.Commands.Product.CreateProduct;
using Application.Features.Commands.Product.Remove;
using Application.Features.Commands.Product.Update;
using Application.Features.Commands.ProductImage.ChangeShowcase;
using Application.Features.Commands.ProductImage.Remove;
using Application.Features.Commands.ProductImage.Upload;
using Application.Features.Queries.Product.GetAll;
using Application.Features.Queries.Product.GetById;
using Application.Features.Queries.ProductImage.GetProductImages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : Controller
{
    private readonly IMediator _mediator;
    private readonly IProductService _productService;

    public ProductsController(IMediator mediator, IProductService productService)
    {
        _mediator = mediator;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
    {
       GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
       return Ok(response);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
    {
        GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
        return Ok(response);
    }

    [HttpGet("qrcode/{productId}")]
    public async Task<IActionResult> GetQrCodeToProduct([FromRoute] string productId)
    {
        var data = await _productService.QrCodeToProductAsync(productId);
        return File(data, "image/png");
    }


    [HttpPost]
    //[Authorize(AuthenticationSchemes = "Admin")]
    //[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Add product")]
    public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
    {
        await _mediator.Send(createProductCommandRequest);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Update product")]
    public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
    {
        UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
        return Ok();
    }

    [HttpDelete("{Id}")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Delete Product")]
    public async Task<IActionResult> Remove([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
    {
        RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
        return Ok();
    }
    
    [HttpPost("[action]")]
    //[Authorize(AuthenticationSchemes = "Admin")]
    //[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Upload Product File")]
    public async Task<IActionResult> Upload([FromQuery,FromBody] UploadProductImageCommandRequest uploadProductImageCommandRequest)
    {
        uploadProductImageCommandRequest.Files = Request.Form.Files;
        UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
        return Ok();
    }
    
    [HttpGet("[action]/{id}")]
    //[Authorize(AuthenticationSchemes = "Admin")]
    //[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Reading, Definition = "Get Product Images ")]
    public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
    {
        List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
        return Ok(response);
    }

    [HttpDelete("[action]/{Id}")]
    //[Authorize(AuthenticationSchemes = "Admin")]
    //[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Delete Product Image")]
    public async Task<IActionResult> DeleteProductImage([FromQuery, FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest,
        [FromQuery] string imageId)
    {
        removeProductImageCommandRequest.ImageId = imageId;
        RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
        return Ok();
    }

    [HttpPut("[action]/{imageId}/{productId}")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Change Showcase Image")]
    public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest showcaseImageCommandRequest)
    {
        ChangeShowcaseImageCommandResponse response = await _mediator.Send(showcaseImageCommandRequest);
        return Ok(response);
    }
}