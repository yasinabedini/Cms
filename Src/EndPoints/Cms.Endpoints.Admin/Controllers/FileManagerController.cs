using Cms.Domain.Models.News.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IO.Pipelines;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
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

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file, string folder)
        {
            var requestContent = new MultipartFormDataContent();

            var item = new MemoryStream();
            file.CopyTo(item);
            item.Position = 0;

            // Assuming you have the image path            
            var imageContent = new ByteArrayContent(item.ToArray());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            // 'file' corresponds to the name of the form field in your API
            requestContent.Add(imageContent, "file", Path.GetFileName(file.FileName));

            // Replace 'your_api_endpoint' with the actual endpoint where you want to send the image
            var response =await _FileManager.PostAsync($"/api/FileManager/upload?folder={folder}", requestContent);

            var imageName = response.Headers.First(t => t.Key == "fileName").Value.First();
            
            Response.Headers.Add("fileName",imageName);            
            return Ok(imageName);
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var requestContent = new MultipartFormDataContent();

            var item = new MemoryStream();
            file.CopyTo(item);
            item.Position = 0;

            // Assuming you have the image path            
            var imageContent = new ByteArrayContent(item.ToArray());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            // 'file' corresponds to the name of the form field in your API
            requestContent.Add(imageContent, "file", Path.GetFileName(file.FileName));

            // Replace 'your_api_endpoint' with the actual endpoint where you want to send the image
            var response = await _FileManager.PostAsync($"/api/FileManager/UploadFileToGallery", requestContent);

            item.Dispose();
            return Ok(response.Headers.First(t => t.Key == "fileName").Value);
        }

        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage(string imageName, string folder)
        {
            var response = await _FileManager.GetByteArrayAsync($"api/fileManager/GetImage?imageName={imageName}&&folder={folder}");
            byte[] imageBytes = response;
     
            return File(imageBytes, "image/jpeg");
        }

        [HttpGet("GetFile")]
        public async Task<IActionResult> GetFile(string fileName, int type)
        {
            var response = await _FileManager.GetByteArrayAsync($"api/fileManager/GetFile?fileName={fileName}&&type={type}");
            byte[] imageBytes = response;

            string typeStr;
            string mimType = "";
            if (type is 3)
            {
                typeStr = "image";
                mimType = "image/jpeg";
            }
            else if (type is 5)
            {
                typeStr = "document";
                mimType = "application/pdf";
            }
            else if (type is 4)
            {
                typeStr = "video";
                mimType = "video/mp4";
            }
            else if (type is 6)
            {
                typeStr = "voice";
                mimType = "audio/mpeg";
            }
            else
            {
                typeStr = "";
                mimType = "";
            }

            if (type is 3)
            {
                return File(imageBytes, mimType);
            }
            else
            {
                return File(imageBytes, mimType, fileName);
            }
        }

        [HttpDelete("Delete")]
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
