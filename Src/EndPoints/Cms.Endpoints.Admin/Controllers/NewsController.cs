using Cmd.Application.Models.Language.Commands.CheckLanguageAvailability;
using Cmd.Application.Models.News.Commands.CheckNewsAvailability;
using Cmd.Application.Models.News.Commands.Create;
using Cmd.Application.Models.News.Commands.Delete;
using Cmd.Application.Models.News.Commands.Update;
using Cmd.Application.Models.News.Queries.GetAll;
using Cmd.Application.Models.News.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            if (!_sender.Send(new CheckNewsAvailabilityCommand() { Id = query.Id }).Result)
            {
                return NotFound("News is not available.");
            }

            var result = _sender.Send(query).Result;

            return Ok(result);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateNewsCommand command)
        {
            var result = _sender.Send(command);

            if (!result.IsCompletedSuccessfully)
            {
                return BadRequest(result.Exception.Message);
            }

            return Ok("News Created successfully.");
        }

        [HttpPut("Update")]
        public IActionResult Update(UpdateNewsCommand command)
        {
            if (!_sender.Send(new CheckNewsAvailabilityCommand() { Id = command.Id }).Result)
            {
                return NotFound("News is not available.");
            }

            _sender.Send(command);

            return Ok("News Updated successfully.");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(long id)
        {
            if (!_sender.Send(new CheckNewsAvailabilityCommand() { Id = id }).Result)
            {
                return NotFound("News is not available.");
            }

            _sender.Send(new DeleteNewsCommand { Id = id });

            return Ok("News Deleted successfully.");
        }

    }
}
