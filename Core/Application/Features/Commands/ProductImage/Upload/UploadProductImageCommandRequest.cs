using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands.ProductImage.Upload;

public class UploadProductImageCommandRequest : IRequest<UploadProductImageCommandResponse>
{
    public string Id { get; set; }

    public IFormFileCollection Files { get; set; }
}