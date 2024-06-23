using Cmd.Application.Models.Info.Queries.GetAll;
using Cmd.Application.Models.Info.Queries.GetAllLinks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly ISender _sender;

        public InfoController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("GetInfo")]
        public async Task<IActionResult> GetInfo(GetAllInfoQuery query)
        {
            var result = await _sender.Send(query);
            return Ok(result.QueryResult.FirstOrDefault());
        }

        [HttpPost("GetLinks")]
        public async Task<IActionResult> GetLinks(GetAllLinksQuery query)
        {
            var result = await _sender.Send(query);
            return Ok(result);
        }
    }
}
