using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;

namespace CVCreationPlatform.ResumeService.Implementations;

public class FileService : IFileService
{
    private readonly IConfiguration _configuration;
    private readonly BlobServiceClient _bloblServiceClient;

    public FileService(IConfiguration configuration)
        => (_configuration, _bloblServiceClient) = (configuration, new BlobServiceClient(this._configuration["Azure:Storage:Url"]));

    public async Task<string> UploadImage(IFormFile imageFile)
    {
        BlobContainerClient containerClient;
        containerClient = this._bloblServiceClient.GetBlobContainerClient(this._configuration["Azure:Storage:ContainerName"]);
        await containerClient.CreateIfNotExistsAsync();

        BlockBlobClient blockBlobClient = containerClient.GetBlockBlobClient(Path.GetRandomFileName() + Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName).ToLowerInvariant());

        new FileExtensionContentTypeProvider().TryGetContentType(imageFile.FileName, out var contentType);
        var blobHttpHeader = new BlobHttpHeaders
        {
            ContentType = (contentType ?? "application/octet-stream").ToLowerInvariant()
        };

        await blockBlobClient.UploadAsync(
            imageFile.OpenReadStream(), 
            new BlobUploadOptions { HttpHeaders = blobHttpHeader });

        return blockBlobClient.Uri.AbsoluteUri;
    }
}
