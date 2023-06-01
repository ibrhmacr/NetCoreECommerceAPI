using Application.Consts;
using Application.CustomAttribute;
using Application.Enums;
using Application.Features.Commands.Basket.AddItemToBasket;
using Application.Features.Commands.Basket.RemoveBasketItem;
using Application.Features.Commands.Basket.UpdateQuantity;
using Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Admin")] //Bu Controlleri acabilmek icin authorize attributinu kullanmak gerekiyor yoksa jwt cozulemeyeceginden dolayi
            //user.identity.name bos gelecektir. 
public class BasketsController : Controller
{
    private readonly IMediator _mediator;

    public BasketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Reading, Definition = "Get Basket Items")] 
    // Attributelar C# nameconvention olarak attribute tagiyle kullanilir fakat kullanirken bu tagi eklememize gerek yoktur.
    public async Task<IActionResult> GetBasketItems([FromQuery]GetBasketItemsQueryRequest getBasketItemsQueryRequest)
    //Bu istegi bodyden geliyor gibi anlasilabir but get isteklerinde parametreden gelicek olan datalar
    //complex type ise body de hata veriyor bu yuzden queryde vererek bu sorun cozulur.
    {
        List<GetBasketItemsQueryResponse> response = await _mediator.Send(getBasketItemsQueryRequest);
        return Ok(response);
    }

    [HttpPost]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Writing, Definition = "Add item to basket")]
    public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
    {
        AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
        return Ok(response);
    }

    [HttpPut]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Updating, Definition = "Update Quantity")]
    public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandRequest)
    {
        UpdateQuantityCommandResponse response = await _mediator.Send(updateQuantityCommandRequest);
        return Ok(response);
    }

    [HttpDelete("{BasketItemId}")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Deleting, Definition = "Remove Basket Item")]
    public async Task<IActionResult> RemoveBasketItem([FromRoute]RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
    {
        RemoveBasketItemCommandResponse response = await _mediator.Send(removeBasketItemCommandRequest);
        return Ok(response);
    }
}