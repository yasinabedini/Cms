using AngleSharp.Io;
using Cmd.Application.Models.File.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;
using Cmd.Application.Security;
using Microsoft.AspNetCore.Cors;

namespace Cms.Endpoints.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecific")]
    public class FileManagerController : ControllerBase
    {
        private readonly HttpClient _FileManager;
        private readonly ISender _sender;
        public FileManagerController(IHttpClientFactory factory, ISender sender)
        {
            _FileManager = factory.CreateClient("FileManager");
            _sender = sender;
        }

        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage(string imageName, string folder)  
        {
            imageName = InputSanitizer.SanitizeInput(imageName);

            if (imageName.Contains(":") || imageName.Contains("..") || imageName.Contains("/") || imageName.Contains("\\"))
            {
                return Ok("Bad Input......");
            }


            var response = await _FileManager.GetByteArrayAsync($"api/fileManager/GetImage?imageName={imageName}&&folder={folder}");
            byte[] imageBytes = response;

            return File(imageBytes, "image/jpeg");
        }

        [HttpGet("GetFile")]
        public async  Task<IActionResult> GetFile(string fileName, int type)
        {
            fileName = InputSanitizer.SanitizeInput(fileName);

            if (fileName.Contains(":") || fileName.Contains("..") || fileName.Contains("/") || fileName.Contains("\\"))
            {
                return Ok("Bad Input......");
            }

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

        [HttpPost("GetFileInfo")]
        public async Task<IActionResult> GetFileInfo(GetFileByIdQuery query)
        {
            var result = await _sender.Send(query);
            
            return Ok(result);
        }
    }
}
