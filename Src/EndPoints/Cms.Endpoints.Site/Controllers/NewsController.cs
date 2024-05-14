using Cmd.Application.Models.News.Commands.CheckNewsAvailability;
using Cmd.Application.Models.News.Queries.GetAll;
using Cmd.Application.Models.News.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ISender _sender;

        public NewsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllNewsQuery query)
        {
            var result = _sender.Send(query).Result;

            var queryResults = result.QueryResult;
            result = new Cmd.Application.Common.Queries.PagedData<Cmd.Application.Models.News.Queries.Common.NewsViewModel>
            {
                QueryResult = queryResults.Where(t => t.IsEnable).ToList(),
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };

            return Ok(result);
        }

        [HttpPost("GetById")]
        public IActionResult GetById(GetNewsByIdQuery query)
        {
            if (!_sender.Send(new CheckNewsAvailabilityCommand() { Id = query.Id }).Result)
            {
                return NotFound("News is not available.");
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
