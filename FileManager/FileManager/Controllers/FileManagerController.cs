using FileManager.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {

        [HttpPost("Upload")]
        public IActionResult Upload([FromForm] IFormFile image, string folder)
        {
            if (image is null)
            {
                return NotFound();
            }


            if (folder is not "news" && folder is not "sweeper")
            {
                return BadRequest("Invalid Folder Name.");
            }


            int maxLength = 5 * 1024 * 1025;
            if (image.Length > maxLength)
            {
                return BadRequest("the max of image size is 5 MG.");
            }

            string extension = Path.GetExtension(image.FileName).ToLower();
            if (!extension.EndsWith(".png") && !extension.EndsWith(".jpeg") && !extension.EndsWith(".jpg"))
            {
                return BadRequest("Invalid Image Format.");
            }
     
            string imageName = Guid.NewGuid().ToString() + extension;

            FileTools.SaveImage(image, imageName, folder, false);

            return Ok(imageName);
        }
    }
}
