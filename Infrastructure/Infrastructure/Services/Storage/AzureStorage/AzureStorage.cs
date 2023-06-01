using Application.Abstractions.Storage.Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Storage.AzureStorage;

public class AzureStorage : Storage, IAzureStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    BlobContainerClient _blobContainerClient;

    public AzureStorage(BlobServiceClient blobServiceClient, BlobContainerClient blobContainerClient)
    {
        _blobServiceClient = blobServiceClient; // Azure daki Connection stringi new leyip veriyoruz.
        
    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, 
        IFormFileCollection files)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        List<(string fileName, string pathOrContainer)> datas = new();
        foreach (IFormFile file in files)
        {
            string fileNewName = await FileRenameAsync(containerName, file.Name, HasFile);
            
            BlobClient blobClient =_blobContainerClient.GetBlobClient(fileNewName);
            await blobClient.UploadAsync(file.OpenReadStream());
            datas.Add((fileNewName, $"{containerName}/{fileNewName}"));
        }

        return datas;
    }

    public async Task DeleteAsync(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient =  _blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteAsync();
    }

    public List<string> GetFiles(string containerName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
    }

    public bool HasFile(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
    }
}