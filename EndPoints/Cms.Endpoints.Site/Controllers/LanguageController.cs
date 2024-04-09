
using Cmd.Application.Models.Language.Queries.GetAll;
using Cmd.Application.Models.Language.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ISender _sender;

        public LanguageController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllLanguageQuery query)
        {
            var result = _sender.Send(query).Result.QueryResult.Where(t => t.IsEnable).ToList();

            return Ok(result);
        }

        [HttpPost("GetById")]
        public IActionResult GetById(GetLanguageByIdQuery query)
        {
            var result = _sender.Send(query).Result;

            if (!result.IsEnable || result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
