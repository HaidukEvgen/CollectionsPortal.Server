using Azure.Storage.Blobs;
using CollectionsPortal.Server.BusinessLayer.Services.Interfaces;
using CollectionsPortal.Server.BusinessLayer.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CollectionsPortal.Server.BusinessLayer.Services.Implementations
{
    public class AzureSerivce : IAzureService
    {
        private readonly AzureBlobServiceOptions _azureBlobServiceOptions;

        public AzureSerivce(IOptions<AzureBlobServiceOptions> azureBlobServiceOptions)
        {
            _azureBlobServiceOptions = azureBlobServiceOptions.Value;
        }

        public async Task<string> UploadImageToAzureBlobStorage(IFormFile imageFile)
        {
            var blobServiceClient = new BlobServiceClient(_azureBlobServiceOptions.ConnectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_azureBlobServiceOptions.ContainerName);

            var blobName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            var blobClient = blobContainerClient.GetBlobClient(blobName);

            using var stream = imageFile.OpenReadStream();
            await blobClient.UploadAsync(stream, true);

            return blobClient.Uri.ToString();
        }
    }
}
