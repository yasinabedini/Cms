using Cmd.Application.Models.User.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("GetAllUser")]
        public async Task<IActionResult> GetAll(GetAllUserQuery query)
        {
            var result = await _sender.Send(query);

            return Ok(result);
        }
    }
}
