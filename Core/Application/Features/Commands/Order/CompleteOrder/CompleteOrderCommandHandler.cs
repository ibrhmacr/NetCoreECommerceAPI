using Application.Abstractions.Services;
using Application.DTOs.Order;
using MediatR;

namespace Application.Features.Commands.Order.CompleteOrder;

public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
{
    private readonly IOrderService _orderService;
    private readonly IMailService _mailService;

    public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService)
    {
        _orderService = orderService;
        _mailService = mailService;
    }


    public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
    {
        (bool succeeded, CompletedOrderDTO dto) = await _orderService.CompleteOrderAsync(request.Id); //Isim vermessek direkt olarak aldiklari veriyi isleyecegimizden dolayi isimvermemizie gerek kalmaz.
        if (succeeded)
            _mailService.SendCompletedOrderMailAsync(dto.Email, dto.OrderCode, dto.OrderDate, dto.UserName, dto.UserSurname);
            
        return new();
    }
}