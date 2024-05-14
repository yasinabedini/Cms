using Cmd.Application.Models.File.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace Cms.Endpoints.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        private readonly HttpClient _FileManager;
        private readonly ISender _sender;
        public FileManagerController(IHttpClientFactory factory, ISender sender)
        {
            _FileManager = factory.CreateClient("FileManager");
            _sender = sender;
        }

        [HttpGet("GetMainImage")]
        public async Task<IActionResult> GetImage(string imageName, string folder)
        {
            var response = await _FileManager.GetByteArrayAsync($"api/fileManager/GetImage?imageName={imageName}&&folder={folder}");
            byte[] imageBytes = response;

            return File(imageBytes, "image/jpeg");
        }

        [HttpGet("GetFile")]
        public async  Task<IActionResult> GetFile(string fileName, string type)
        {
            var response = await _FileManager.GetByteArrayAsync($"api/fileManager/GetFile?fileName={fileName}&&type={type}");
            byte[] imageBytes = response;

            return File(imageBytes, "image/jpeg");                
        }

        [HttpPost("GetFileInfo")]
        public async Task<IActionResult> GetFileInfo(GetFileByIdQuery query)
        {
            var result = await _sender.Send(query);
            
            return Ok(result);
        }
    }
}
