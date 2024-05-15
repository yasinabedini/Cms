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
    }
}
