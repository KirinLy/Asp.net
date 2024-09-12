using Microsoft.AspNetCore.Http;

namespace AzureBlob.Services
{
    public interface IContainerService
    {
        public Task CreateContainerAsync(string containerName);
        public Task<List<string>> GetContainerNames();
    }
}
