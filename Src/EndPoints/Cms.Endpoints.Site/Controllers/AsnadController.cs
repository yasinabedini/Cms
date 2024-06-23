using Cmd.Application.Models.News.Queries.GetAboutWithFilter;
using Cmd.Application.Models.News.Queries.GetAsnad;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecific")]
    public class AsnadController : ControllerBase
    {

        private readonly ISender _sender;

        public AsnadController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetMuseumAsnad(GetAsnadQuery query)
        {
            var result = await _sender.Send(query);

            return Ok(result);
        }
    }
}
