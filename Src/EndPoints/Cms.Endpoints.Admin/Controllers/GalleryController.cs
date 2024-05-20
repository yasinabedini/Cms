using Cmd.Application.Models.File.Commands.Create;
using Cmd.Application.Models.File.Commands.Delete;
using Cmd.Application.Models.File.Queries.GetAllFileTypes;
using Cmd.Application.Models.Gallery.Commands.Create;
using Cmd.Application.Models.Gallery.Queries.GetNewsGallery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly ISender _sender;

        public GalleryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("GetNewsGallery")]
        public async Task<IActionResult> GetNewsGallery(GetNewsGalleryQuery query)
        {
            var result = await _sender.Send(query);

            return Ok(result);
        }

        [HttpPost("CreateGallery")]
        public async Task<IActionResult> CreateGallery(CreateGalleryCommand command)
        {
            await _sender.Send(command);

            return Ok("gallery Created Successfully..");
        }

        [HttpPost("AddFile")]
        public async Task<IActionResult> AddFile(CreateFileCommand command)
        {
             await _sender.Send(command);

            return Ok("Created Successfuly");
        }

        [HttpGet("GetAllFileTypes")]
        public async Task<IActionResult> GetAllFileTypes()
        {
            var result = await _sender.Send(new GetAllFileTypesQuery());
            return Ok(result);
        }

        [HttpDelete("DeleteFile")]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            await _sender.Send(new DeleteFileCommand {fileName = fileName });

            return Ok("File Deleted Successfully");
        }
    }
}
