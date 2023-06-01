using Application.Consts;
using Application.CustomAttribute;
using Application.Enums;
using Application.Features.Commands.Order.CompleteOrder;
using Application.Features.Commands.Order.Create;
using Application.Features.Queries.Order.GetAll;
using Application.Features.Queries.Order.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Admin")]
public class OrdersController : Controller
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get Order By Id")]
    public async Task<IActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest getOrderByIdQueryRequest)
    {
        GetOrderByIdQueryResponse response = await _mediator.Send(getOrderByIdQueryRequest);
        return Ok(response);
    }
    
    [HttpGet]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get all orders")]
    public async Task<IActionResult> GetAllOrders([FromQuery]GetAllOrdersQueryRequest getAllOrdersQueryRequest)
    {
        GetAllOrdersQueryResponse response = await _mediator.Send(getAllOrdersQueryRequest);
        return Ok(response);
    }
    
    [HttpPost]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Writing, Definition = "Create Order")]
    public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
    {
        CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
        return Ok(response);
    }

    [HttpGet("complete-order/{Id}")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Updating, Definition = "Complete Order")]
    public async Task<IActionResult> CompleteOrder([FromRoute]CompleteOrderCommandRequest completeOrderCommandRequest)
    {
        CompleteOrderCommandResponse response = await _mediator.Send(completeOrderCommandRequest);
        return Ok(response);  
    }
    //1. Davranis- Client tarafindan siparis tamamlandigina dair bir istek geldikten sonra
    //statusu 200 ise daha sonra kargo mail endpointi tetiklenebilir.
      

}