using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Extensions;
using RDTrackR.Domain.Services.Storage;
using RDTrackR.Domain.ValueObjects;

namespace RDTrackR.Infrastructure.Services.Storage
{
    public class AzureStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public AzureStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task Delete(User user, string filename)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(user.UserIdentifier.ToString());

            var exist = await containerClient.ExistsAsync();
            if (exist.Value)
            {
                await containerClient.DeleteBlobIfExistsAsync(filename);
            }
        }

        public async Task DeleteContainer(Guid userIdentifier)
        {
            var container = _blobServiceClient.GetBlobContainerClient(userIdentifier.ToString());
            await container.DeleteIfExistsAsync();
        }

        public async Task<string> GetFileUrl(User user, string filename)
        {
            var containerName = user.UserIdentifier.ToString();

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var exist = await containerClient.ExistsAsync();

            if (exist.Value.IsFalse())
                return string.Empty;

            var blobClient = containerClient.GetBlobClient(filename);

            exist = await blobClient.ExistsAsync();

            if (exist.Value)
            {
                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = containerName,
                    BlobName = filename,
                    Resource = "b",
                    ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(MyRecipeBookRuleConstants.MAXIMUM_IMAGE_URL_LIFETIME_IN_MINUTES)
                };

                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                return blobClient.GenerateSasUri(sasBuilder).ToString();
            }

            return string.Empty;
        }

        public async Task Upload(User user, Stream file, string filename)
        {
            var container = _blobServiceClient.GetBlobContainerClient(user.UserIdentifier.ToString());

            await container.CreateIfNotExistsAsync();

            var bobClient = container.GetBlobClient(filename);

            await bobClient.UploadAsync(file, overwrite: true);
        }
    }
}
