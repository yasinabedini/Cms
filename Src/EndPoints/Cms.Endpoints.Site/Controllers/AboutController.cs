using Cmd.Application.Models.News.Queries.GetAboutMuseum;
using Cmd.Application.Models.News.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {

        private readonly ISender _sender;

        public AboutController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAboutMuseum()
        {
            var result = _sender.Send(new GetAboutMuseumQuery()).Result;
           
            return Ok(result);
        }
    }
}
