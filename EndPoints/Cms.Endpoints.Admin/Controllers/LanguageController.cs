using Cmd.Application.Models.Language.Commands.Create;
using Cmd.Application.Models.Language.Commands.Delete;
using Cmd.Application.Models.Language.Commands.Update;
using Cmd.Application.Models.Language.Queries.GetAll;
using Cmd.Application.Models.Language.Queries.GetById;
using Cms.Domain.Models.Language.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _sender.Send(new GetLanguageByIdQuery { Id = id }).Result;

            return Ok(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _sender.Send(new GetAllLanguageQuery { PageNumber = 1, PageSize = 200 }).Result;

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
            _sender.Send(command);

            return Ok("Language Updated Successfuly.");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(DeleteLanguageCommand command)
        {
            _sender.Send(command);

            return Ok("Language Deleted Successfuly.");
        }
    }
}
