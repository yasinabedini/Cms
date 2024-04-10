using Cmd.Application.Models.Info.Commands.CheckInfoAvailability;
using Cmd.Application.Models.Language.Commands.CheckLanguageAvailability;
using Cmd.Application.Models.Language.Commands.Create;
using Cmd.Application.Models.Language.Commands.Delete;
using Cmd.Application.Models.Language.Commands.Update;
using Cmd.Application.Models.Language.Queries.GetAll;
using Cmd.Application.Models.Language.Queries.GetById;
using Cms.Domain.Models.Language.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Cms.Endpoints.Admin.Controllers
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

        [HttpPost("GetById")]
        public IActionResult GetById(GetLanguageByIdQuery query)
        {
            if (!_sender.Send(new CheckLanguageAvailabilityCommand() { Id = query.Id }).Result)
            {
                return NotFound("Language is not available.");
            }

            var result = _sender.Send(query).Result;
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllLanguageQuery query)
        {
            string name = Directory.GetCurrentDirectory();

            var result = _sender.Send(query).Result;

            return Ok(result);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateLanguageCommand command)
        {
            _sender.Send(command);

            return Ok("Language Created Successfuly.");
        }

        [HttpPut("Update")]
        public IActionResult Update(UpdateLanguageCommand command)
        {
            if (!_sender.Send(new CheckLanguageAvailabilityCommand() { Id = command.Id }).Result)
            {
                return NotFound("Language is not available.");
            }

            _sender.Send(command);

            return Ok("Language Updated Successfully.");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            if (!_sender.Send(new CheckLanguageAvailabilityCommand() { Id = id }).Result)
            {
                return NotFound("Language is not available.");
            }

            _sender.Send(new DeleteLanguageCommand { Id = id });

            return Ok("Language Deleted Successfuly.");
        }
    }
}
