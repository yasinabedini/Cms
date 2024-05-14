using Cmd.Application.Models.Contact.Commands.CheckContactAvailability;
using Cmd.Application.Models.Contact.Commands.Delete;
using Cmd.Application.Models.Contact.Queries.GetAll;
using Cmd.Application.Models.Contact.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ISender _sender;

        public ContactController(ISender sender)
        {
            _sender = sender;
        }



        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllContactQuery query)
        {
            var result = _sender.Send(query).Result;

            return Ok(result);
        }

        [HttpPost("GetById")]
        public IActionResult GetById(GetContactByIdQuery query)
        {
            if (!_sender.Send(new CheckContactAvailabilityCommand() { Id = query.Id }).Result)
            {
                return NotFound("Contact is not available.");
            }

            var result = _sender.Send(query).Result;
        
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(long id)
        {
            if (!_sender.Send(new CheckContactAvailabilityCommand() { Id = id }).Result)
            {
                return NotFound("Contact is not available.");
            }

            _sender.Send(new DeleteContactCommand { Id = id });

            return Ok("Contact Deleted Successfully.");
        }


    }
}
