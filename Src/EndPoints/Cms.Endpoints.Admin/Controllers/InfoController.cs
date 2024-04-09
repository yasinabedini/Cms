using Cmd.Application.Models.Info.Commands.Create;
using Cmd.Application.Models.Info.Commands.Delete;
using Cmd.Application.Models.Info.Commands.Update;
using Cmd.Application.Models.Info.Queries.GetAll;
using Cmd.Application.Models.Info.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
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
            _sender.Send(command);

            return Ok("Info Updated Successfully.");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(long id)
        {
            _sender.Send(new DeleteInfoCommand { Id = id });

            return Ok("Info Deleted Successfully.");
        }
    }
}
