using Application.Repositories.ProductImageFile;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Commands.ProductImage.ChangeShowcase;

public class ChangeShowcaseImageCommandHandler : IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
{
    private readonly IProductImageFileWriteRepository _fileWriteRepository;

    public ChangeShowcaseImageCommandHandler(IProductImageFileWriteRepository fileWriteRepository)
    {
        _fileWriteRepository = fileWriteRepository;
    }

    public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
    {
        var query = _fileWriteRepository.Table
            .Include(p => p.Products)
            .SelectMany(p => p.Products, (pif, p) => new
            {
                pif,
                p
            });

        var data = await query.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(request.ProductId) && p.pif.Showcase);
        if (data != null)
            data.pif.Showcase = false;
        
        var image = await query.FirstOrDefaultAsync(p => p.pif.Id == Guid.Parse(request.ImageId));
        if (image!=null)
            image.pif.Showcase = true;
        
        await _fileWriteRepository.SaveAsync();
        
        return new();
    }
}