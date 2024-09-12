using Azure.Storage.Blobs;

namespace AzureBlob.Services
{
    public class ContainerService : IContainerService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public ContainerService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task CreateContainerAsync(string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
        }

        public async Task<List<string>> GetContainerNames()
        {
            var containerNames = new List<string>();
            await foreach (var containerItem in _blobServiceClient.GetBlobContainersAsync())
            {
                containerNames.Add(containerItem.Name);
            }

            return containerNames;
        }
    }
}
