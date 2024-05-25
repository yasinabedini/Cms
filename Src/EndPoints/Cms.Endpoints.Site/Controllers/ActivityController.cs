using Cmd.Application.Models.News.Queries.GetActivity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly ISender _sender;

        public ActivityController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("GetActivity")]
        public async Task<IActionResult> GetActivity(GetActivityQuery query)
        {
            var result = await _sender.Send(query);

            return Ok(result);
        }
    }
}
