using AzureBlob.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlob.Controllers
{
    public class BlobContainerController : Controller
    {
        private readonly IContainerService _containerService;

        public BlobContainerController(IContainerService containerService)
        {
            _containerService = containerService;
        }

        public async Task<IActionResult> Index()
        {
            var containers = await _containerService.GetContainerNames();
            return View(containers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string containerName)
        {
            await _containerService.CreateContainerAsync(containerName);
            return RedirectToAction(nameof(Index));
        }
    }
}
