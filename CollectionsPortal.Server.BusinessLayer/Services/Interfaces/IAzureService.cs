using Microsoft.AspNetCore.Http;

namespace CollectionsPortal.Server.BusinessLayer.Services.Interfaces
{
    public interface IAzureService
    {
        public Task<string> UploadImageToAzureBlobStorage(IFormFile imageFile);
    }
}
