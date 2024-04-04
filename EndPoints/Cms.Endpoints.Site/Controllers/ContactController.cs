using Cmd.Application.Models.Contact.Commands.Create;
using Cmd.Application.Models.Info.Queries.GetById;
using Cmd.Application.Models.Info.Queries.GetByLanguageId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Site.Controllers
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

        [HttpPost("ContactUs")]
        public IActionResult ContactUs(CreateContactCommand command)
        {
            _sender.Send(command);

            return Ok("Contact Created Successfully");
        }

        [HttpPost("GetInfo")]
        public IActionResult GetInfo(GetInfoByLanguageIdQuery query)
        {
            var result = _sender.Send(query).Result;

            return Ok(result);
        }
    }
}
