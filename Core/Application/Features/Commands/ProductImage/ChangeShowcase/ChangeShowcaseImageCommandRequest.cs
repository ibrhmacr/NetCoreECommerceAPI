using MediatR;

namespace Application.Features.Commands.ProductImage.ChangeShowcase;

public class ChangeShowcaseImageCommandRequest : IRequest<ChangeShowcaseImageCommandResponse>
{
    public string ProductId { get; set; }

    public string ImageId { get; set; }
}