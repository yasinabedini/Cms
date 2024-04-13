using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        private readonly HttpClient _FileManager;
        public FileManagerController(IHttpClientFactory factory)
        {
            _FileManager = factory.CreateClient("FileManager");
        }

        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage(string imageName, string folder)
        {
            var response = await _FileManager.GetByteArrayAsync($"api/fileManager/GetImage?imageName={imageName}&&folder={folder}");
            byte[] imageBytes = response;

            return File(imageBytes, "image/jpeg");
        }
    }
}
