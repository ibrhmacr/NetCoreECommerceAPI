using Application.Abstractions.Storage;
using Application.Repositories;
using Application.Repositories.ProductImageFile;
using Domain.Entities;
using MediatR;


namespace Application.Features.Commands.ProductImage.Upload;

public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest,UploadProductImageCommandResponse>
{
    private readonly IStorageService _storageService;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

    public UploadProductImageCommandHandler(IStorageService storageService, IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
    {
        _storageService = storageService;
        _productReadRepository = productReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
    }

    public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        List<(string fileName, string pathOrContainerName)> result = await _storageService
            .UploadAsync("photo-images", request.Files);

        Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
        //For each ile product.ProductImageFiles.Add(); gelen filelarin icinde donerek ef kullanilarak ekleme islemi de yapilabilir.

        await _productImageFileWriteRepository.AddRangeAsync(result.Select(r =>
            new ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                StorageType = _storageService.StorageName,
                Products = new List<Domain.Entities.Product>() { product }
            }).ToList());
        await _productImageFileWriteRepository.SaveAsync();
        return new();
        //todo category ekledikten sonra eklenen image dosylarinida kategorilere gore eklemenmesi
    }
}