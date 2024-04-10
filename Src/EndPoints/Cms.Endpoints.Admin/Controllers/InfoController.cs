using Cmd.Application.Models.Info.Commands.CheckInfoAvailability;
using Cmd.Application.Models.Info.Commands.Create;
using Cmd.Application.Models.Info.Commands.Delete;
using Cmd.Application.Models.Info.Commands.Update;
using Cmd.Application.Models.Info.Queries.GetAll;
using Cmd.Application.Models.Info.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly ISender _sender;

        public InfoController(ISender sender)
        {
            _sender = sender;
        }


        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllInfoQuery query)
        {
            var result = _sender.Send(query).Result;

            return Ok(result);
        }

        [HttpPost("GetById")]
        public IActionResult GetById(GetInfoByIdQuery query)
        {
            if (!_sender.Send(new CheckInfoAvailabilityCommand() { Id = query.Id }).Result)
            {
                return NotFound("Info is not available.");
            }

            var result = _sender.Send(query).Result;

            return Ok(result);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateInfoCommand command)
        {
            _sender.Send(command);

            return Ok("Info Crated SuccessFully.");
        }

        [HttpPut("UpdateInfo")]
        public IActionResult UpdateInfo(UpdateInfoCommand command)
        {
            if (!_sender.Send(new CheckInfoAvailabilityCommand() { Id = command.Id }).Result)
            {
                return NotFound("Info is not available.");
            }

            _sender.Send(command);

            return Ok("Info Updated Successfully.");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(long id)
        {
            if (!_sender.Send(new CheckInfoAvailabilityCommand() { Id = id }).Result)
            {
                return NotFound("Info is not available.");
            }

            _sender.Send(new DeleteInfoCommand { Id = id });

            return Ok("Info Deleted Successfully.");
        }
    }
}
