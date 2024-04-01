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

        [HttpGet("GetById")]
        public IActionResult GetById(int id, int languageId)
        {
            var result = _sender.Send(new GetSweeperByIdQuery { Id = id }).Result;

            return Ok(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll(int languageId)
        {
            var result = _sender.Send(new GetAllSweeperForWebQuery { LanguageId = languageId, PageNumber = 1, PageSize = 200 });

            return Ok(result.Result);
        }
    }
}
