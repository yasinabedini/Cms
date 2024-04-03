using Cmd.Application.Models.Sweeper.Queries.GetAll;
using Cmd.Application.Models.Sweeper.Queries.GetAllForWeb;
using Cmd.Application.Models.Sweeper.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Web.Controllers
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
            var result = _sender.Send(query).Result;

            if (!result.IsEnable || result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllSweeperQuery query)
        {
            var result = _sender.Send(query).Result.QueryResult.Where(t => t.IsEnable);

            return Ok(result);
        }
    }
}
