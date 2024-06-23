using Cmd.Application.Models.Sms.Commands;
using Cmd.Application.Models.User.Commands.ConfirmPhone;
using Cmd.Application.Models.User.Commands.Login;
using Cmd.Application.Models.User.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Cms.Endpoints.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISender _sender;

        public AccountController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await _sender.Send(command);
            if (result) return Ok("User Registration Is Successfully");
            else return BadRequest("User Registration Is Failed ): Maybe a user has already registered with this mobile phone");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _sender.Send(command);

            return Ok(new { result = result.Item1, message = result.Item2 });
        }

        [HttpPost("ConfirmPhoneNumber")]
        public async Task<IActionResult> ConfirmPhoneNumber(ConfirmPhoneCommand command)
        {
            var result = await _sender.Send(command);
            return Ok(result);
        }

        [HttpPost("SendSms")]
        public async Task<IActionResult> SendSms(SendSmsCommand command)
        {
            await _sender.Send(command);

            return Ok("Sms Send Successfully...");
        }
    }
}
