using Azure.Storage.Blobs;
using EmployeeManagementAPI.Configurations;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace EmployeeManagementAPI.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly AzureStorageSettings _setting;
        public BlobStorageService(IOptions<AzureStorageSettings> options)
        {
            _setting = options.Value;
        }
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var blobServiceClient = new BlobServiceClient(_setting.ConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_setting.ContainerName);
            await containerClient.CreateIfNotExistsAsync();

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var blobClient = containerClient.GetBlobClient(fileName);

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(
            stream,
            overwrite: true);

            return blobClient.Uri.ToString();

        }
    }
}
