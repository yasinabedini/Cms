using Cmd.Application.Models.News.Commands.CheckNewsAvailability;
using Cmd.Application.Models.News.Commands.CheckNewsTypeAvailability;
using Cmd.Application.Models.News.Commands.CreateNewsType;
using Cmd.Application.Models.News.Commands.DeleteNewsType;
using Cmd.Application.Models.News.Commands.UpdateNewsType;
using Cmd.Application.Models.News.Queries.GetAllNewsType;
using Cmd.Application.Models.News.Queries.GetNewsTypeById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            if (!_sender.Send(new CheckNewsTypeAvailabilityCommand() { Id = query.Id }).Result)
            {
                return NotFound("News Type is not available.");
            }

            var result = _sender.Send(query).Result;

            return Ok(result);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateNewsTypeCommand command)
        {
            _sender.Send(command);

            return Ok("News Type Created Successfully.");
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateNewsTypeCommand command)
        {
            if (!await _sender.Send(new CheckNewsTypeAvailabilityCommand() { Id = command.Id }))
            {
                return NotFound("News Type is not available.");
            }

            await _sender.Send(command);

            return Ok("News Type Updated Successfully.");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            if (!_sender.Send(new CheckNewsTypeAvailabilityCommand() { Id = id }).Result)
            {
                return NotFound("News Type is not available.");
            }

            _sender.Send(new DeleteNewsTypeCommand { Id = id });

            return Ok("News Type Deleted Successfully.");
        }
    }
}
