using Cmd.Application.Models.Sweeper.Commands.CheckSweeperAvailability;
using Cmd.Application.Models.Sweeper.Queries.GetAll;
using Cmd.Application.Models.Sweeper.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SweeperController : ControllerBase
    {
        private readonly ISender _sender;

        public SweeperController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("GetById")]
        public IActionResult GetById(GetSweeperByIdQuery query)
        {
            if (!_sender.Send(new CheckSweeperAvailabilityCommand() { Id = query.Id }).Result)
            {
                return NotFound("Sweeper is not available.");
            }
            var result = _sender.Send(query).Result;

            if (!result.IsEnable)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllSweeperQuery query)
        {
            var result = _sender.Send(query).Result;
            var queryResults = result.QueryResult;
            result = new Cmd.Application.Common.Queries.PagedData<Cmd.Application.Models.Sweeper.Queries.Common.SweeperViewModel>
            {
                QueryResult = queryResults.Where(t => t.IsEnable).ToList(),
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
            return Ok(result);
        }
    }
}
