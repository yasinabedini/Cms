using Cmd.Application.Models.News.Commands.CreateNewsType;
using Cmd.Application.Models.News.Commands.DeleteNewsType;
using Cmd.Application.Models.News.Commands.UpdateNewsType;
using Cmd.Application.Models.News.Queries.GetAllNewsType;
using Cmd.Application.Models.News.Queries.GetNewsTypeById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsTypeController : ControllerBase
    {
        private readonly ISender _sender;

        public NewsTypeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllNewsTypeQuery query)
        {
            var result = _sender.Send(query).Result;

            return Ok(result);
        }

        [HttpPost("GetById")]
        public IActionResult GetById(GetNewsTypeByIdQuery query)
        {
            var result = _sender.Send(query).Result;
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateNewsTypeCommand command)
        {
            _sender.Send(command);

            return Ok("News Type Created Successfully.");
        }

        [HttpPut("Update")]
        public IActionResult Update(UpdateNewsTypeCommand command)
        {
            _sender.Send(command);

            return Ok("News Type Updated Successfully.");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            _sender.Send(new DeleteNewsTypeCommand { Id = id});

            return Ok("News Type Deleted Successfully.");
        }
    }
}
