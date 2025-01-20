using FileManager.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json;

namespace FileManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, string folder)
        {
            if (file is null)
            {
                return NotFound();
            }


            if (folder is not "news" && folder is not "sweeper" && folder is not "gallery" && folder is not "attachment" )
            {
                return BadRequest("Invalid Folder Name.");
            }

            int maxLength = 104857600;
            if (file.Length > maxLength)
            {
                return BadRequest("the max of image size is 5 MG.");
            }

            string extension = Path.GetExtension(file.FileName).ToLower();

            string imageName = Guid.NewGuid().ToString() + extension;

            if (folder is "attachment")
            {
                string type = "";
                if (extension is ".png" || extension is ".jpg" || extension is ".jpeg")
                {
                    type = "image";
                }
                if (extension is ".mp4" || extension is ".mkv")
                {
                    type = "video";
                }
                if (extension is ".pdf" || extension is ".zip" || extension is ".rar" || extension is ".txt" || extension is ".docx" || extension is ".pptx" || extension is ".xlsx")
                {
                    type = "document";
                }
                if (extension is ".mp3" || extension is ".ogg")
                {
                    type = "voice";
                }


                string imagePath = imagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\img\\attachment\\{type}", imageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                if (type is "image")
                {
                    FileTools.SaveGalleryImageThumb(imageName);
                }
            }

            else
            {
                FileTools.SaveImage(file, imageName, folder, true);
            }

            var imageNameJson = JsonSerializer.Serialize(imageName);

            Response.Headers.Add("fileName", imageName);
            return Ok(imageNameJson);
        }

        [HttpPost("UploadFile")]
        public IActionResult UploadFile([FromForm] IFormFile file)
        {
            if (file is null)
            {
                return NotFound();
            }


            int maxLength = 5 * 1024 * 1025;
            if (file.Length > maxLength)
            {
                return BadRequest("the max of image size is 5 MG.");
            }

            string extension = Path.GetExtension(file.FileName).ToLower();

            string imageName = Guid.NewGuid().ToString() + extension;

            string imagePath = imagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\img\\gallery\\", imageName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var imageNameJson = JsonSerializer.Serialize(imageName);

            Response.Headers.Add("imageName", imageName);
            return Ok();
        }

        [HttpGet("GetImage")]
        public IActionResult GetImage(string imageName, string folder)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", folder, imageName);

            if (!Path.Exists(filePath)) return BadRequest();

            byte[] imageBytes = System.IO.File.ReadAllBytes($@"wwwroot/img/{folder}/{imageName}");
            return File(imageBytes, "image/jpeg");
        }

        [HttpGet("GetFile")]
        public IActionResult GetFile(string fileName, int type)
        {            
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

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "attachment", typeStr, fileName);

            if (!Path.Exists(filePath))
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "gallery", fileName);
                if (!Path.Exists(filePath)) return BadRequest();
            }

            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);

           


            if (type is 3)
            {
                return File(imageBytes, mimType);
            }
            else
            {
                return File(imageBytes, mimType,fileName);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string imageName, string folder)
        {
            if (FileTools.DeleteFile(folder, imageName))
            {
                return Ok("File Deleted Successfully.");
            }
            else
            {
                return BadRequest("File Not Found.");
            }

        }
    }
}
