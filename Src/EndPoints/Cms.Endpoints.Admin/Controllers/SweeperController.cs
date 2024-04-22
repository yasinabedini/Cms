using Cmd.Application.Models.News.Commands.CheckNewsTypeAvailability;
using Cmd.Application.Models.Sweeper.Commands.CheckSweeperAvailability;
using Cmd.Application.Models.Sweeper.Commands.Create;
using Cmd.Application.Models.Sweeper.Commands.Delete;
using Cmd.Application.Models.Sweeper.Commands.Update;
using Cmd.Application.Models.Sweeper.Queries.GetAll;
using Cmd.Application.Models.Sweeper.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cms.Endpoints.Admin.Controllers
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
            if (!_sender.Send(new CheckSweeperAvailabilityCommand() { Id =query.Id }).Result)
            {
                return NotFound("Sweeper is not available.");
            }

            var result = _sender.Send(query).Result;
          
            return Ok(result);
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllSweeperQuery query)
        {
            var result = _sender.Send(query);

            return Ok(result.Result);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateSweeperCommand command)
        {
            _sender.Send(command);

            return Ok("Sweeper Created Successfuly.");
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateSweeperCommand command)
        {
            if (!await _sender.Send(new CheckSweeperAvailabilityCommand() { Id = command.Id }))
            {
                return NotFound("Sweeper is not available.");
            }

            await _sender.Send(command);

            return Ok("Sweeper Updated Successfuly.");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            if (!_sender.Send(new CheckSweeperAvailabilityCommand() { Id = id }).Result)
            {
                return NotFound("Sweeper is not available.");
            }

            _sender.Send(new DeleteSweeperCommand { Id = id });

            return Ok("Sweeper Deleted Successfuly.");
        }

    }
}
