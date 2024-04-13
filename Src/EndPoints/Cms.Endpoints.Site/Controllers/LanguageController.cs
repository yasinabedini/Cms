
using Cmd.Application.Models.Contact.Commands.CheckAvailability;
using Cmd.Application.Models.Language.Commands.CheckLanguageAvailability;
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
            var result = _sender.Send(query).Result;

            var queryResults = result.QueryResult;
            result = new Cmd.Application.Common.Queries.PagedData<Cmd.Application.Models.Language.Queries.Common.LanguageViewModel>
            {
                QueryResult = queryResults.Where(t => t.IsEnable).ToList(),
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };

            return Ok(result);
        }

        [HttpPost("GetById")]
        public IActionResult GetById(GetLanguageByIdQuery query)
        {
            if (!_sender.Send(new CheckLanguageAvailabilityCommand() { Id = query.Id }).Result)
            {
                return NotFound("Language is not available.");
            }

            var result = _sender.Send(query).Result;

            if (!result.IsEnable)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
