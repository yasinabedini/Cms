using Cmd.Application.Models.Info.Commands.AddLink;
using Cmd.Application.Models.Info.Commands.CheckInfoAvailability;
using Cmd.Application.Models.Info.Commands.Create;
using Cmd.Application.Models.Info.Commands.Delete;
using Cmd.Application.Models.Info.Commands.DeleteLink;
using Cmd.Application.Models.Info.Commands.Update;
using Cmd.Application.Models.Info.Commands.UpdateLink;
using Cmd.Application.Models.Info.Queries.GetAll;
using Cmd.Application.Models.Info.Queries.GetAllLinks;
using Cmd.Application.Models.Info.Queries.GetById;
using Cmd.Application.Models.Info.Queries.GetLinkById;
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

        [HttpPost("GetAllLinks")]
        public async Task<IActionResult> GetAllLinks(GetAllLinksQuery query)
        {
            var result = await _sender.Send(query);
            return Ok(result);
        }

        [HttpPost("GetLinkById")]
        public async Task<IActionResult> GetLinkById(GetLinkByIdQuery query)
        {
            var result = await _sender.Send(query);
            if (result.Id is 0)
            {
                return NotFound("Link is not available.");
            }
            return Ok(result);
        }

        [HttpPost("AddLink")]
        public async Task<IActionResult> AddLink(AddLinkCommand query)
        {
            await _sender.Send(query);
            return Ok("Link Created Suucessfuly...");
        }

        [HttpPut("UpdateLink")]
        public async Task<IActionResult> UpdateLink(UpdateLinkCommand query)
        {
            await _sender.Send(query);
            return Ok("Link Updated Suucessfuly...");
        }

        [HttpDelete("DeleteLink")]
        public IActionResult DeleteLink(long id)
        {           
            _sender.Send(new DeleteLinkCommand { Id = id });

            return Ok("Link Deleted Successfully.");
        }

    }
}
