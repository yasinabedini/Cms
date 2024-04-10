using Cms.Domain.Models.News.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IO.Pipelines;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace Cms.Endpoints.Admin.Controllers
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

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile image, string folder)
        {
            var requestContent = new MultipartFormDataContent();

            var item = new MemoryStream();
            image.CopyTo(item);
            item.Position = 0;

            // Assuming you have the image path            
            var imageContent = new ByteArrayContent(item.ToArray());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            // 'file' corresponds to the name of the form field in your API
            requestContent.Add(imageContent, "image", Path.GetFileName(image.FileName));

            // Replace 'your_api_endpoint' with the actual endpoint where you want to send the image
            var response = await _FileManager.PostAsync($"/api/FileManager/upload?folder={folder}", requestContent);

            item.Dispose();
            return Ok(response.Headers.First(t => t.Key == "imageName").Value);
        }

        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage(string imageName, string folder)
        {
            var response = await _FileManager.GetByteArrayAsync($"api/fileManager/GetImage?imageName={imageName}&&folder={folder}");
            byte[] imageBytes = response;
     
            return File(imageBytes, "image/jpeg");
        }

        [HttpDelete("DeleteImage")]
        public async Task<IActionResult> DeleteImage(string imageName, string folder)
        {
            var response = await _FileManager.DeleteAsync($"api/FileManager/Delete?imageName={imageName}&&folder={folder}");
        
            if (response.StatusCode is System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest("File Not Found");
            }

            return Ok("File Deleted SuccessFully");
        }
    }
}
