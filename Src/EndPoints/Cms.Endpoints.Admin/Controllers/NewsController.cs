using Cmd.Application.Models.News.Commands.Create;
using Cmd.Application.Models.News.Commands.Delete;
using Cmd.Application.Models.News.Commands.Update;
using Cmd.Application.Models.News.Queries.GetAll;
using Cmd.Application.Models.News.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Admin.Controllers
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

            return Ok(result);
        }

        [HttpPost("GetById")]
        public IActionResult GetById(GetNewsByIdQuery query)
        {
            var result = _sender.Send(query).Result;
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("Create")]
        [Produces("multipart/form-data")]
        public IActionResult Create(CreateNewsCommand command)
        {
            _sender.Send(command);

            return Ok("News Created successfully.");
        }

        [HttpPut("Update")]
        public IActionResult Update(UpdateNewsCommand command)
        {
            _sender.Send(command);

            return Ok("News Updated successfully.");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(DeleteNewsCommand command)
        {
            _sender.Send(command);

            return Ok("News Deleted successfully.");
        }

    }
}
