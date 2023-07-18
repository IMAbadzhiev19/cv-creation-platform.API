using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using CVCreationPlatform.ResumeService.Contracts.BlobStorage;
using Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CVCreationPlatform.ResumeService.Implementations.BlobStorage;

public class FileService : IFileService
{
    private readonly IConfiguration _configuration;
    private readonly BlobServiceClient _bloblServiceClient;
    private readonly ApplicationDbContext _context;

    public FileService(IConfiguration configuration, ApplicationDbContext context)
    {
        _context = context;
        _configuration = configuration;
        var connectionString = _configuration["Azure:Storage:StorageConnectionString"];
        _bloblServiceClient = new BlobServiceClient(connectionString);
    }

    public async Task<string> UploadImage(IFormFile imageFile, Guid resumeId = default)
    {
        BlobContainerClient containerClient;
        var containerName = _configuration["Azure:Storage:ContainerName"];
        containerClient = _bloblServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        string url = "";

        if (resumeId != default)
        {
            var resume = await _context.Resumes.Include(r => r.PersonalInfo).FirstOrDefaultAsync(x => x.Id == resumeId);
            if (resume != null)
                if (resume.PersonalInfo != null)
                    if (resume.PersonalInfo.PhotoUrl != null)
                        url = resume.PersonalInfo.PhotoUrl;
        }

        string blobName = "";
        if (!string.IsNullOrEmpty(url))
        {
            Uri uri = new Uri(url);
            blobName = uri.Segments.Last();

            BlobClient existingBlobClient = containerClient.GetBlobClient(blobName);
            await existingBlobClient.DeleteIfExistsAsync();
        }

        BlockBlobClient blockBlobClient = containerClient.GetBlockBlobClient(
            blobName == "" ? Path.GetRandomFileName() + Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName).ToLowerInvariant() : blobName);

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
