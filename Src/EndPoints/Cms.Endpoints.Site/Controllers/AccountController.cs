using Cmd.Application.Models.Email.Commands.SendEmail;
using Cmd.Application.Models.Sms.Commands;
using Cmd.Application.Models.User.Commands.Authenticate;
using Cmd.Application.Models.User.Commands.ConfirmPhone;
using Cmd.Application.Models.User.Commands.Login;
using Cmd.Application.Models.User.Commands.RefreshToken;
using Cmd.Application.Models.User.Commands.Register;
using Cmd.Application.Models.User.Commands.SetPassword;
using Cmd.Application.Models.User.Queries.GetAccountInfo;
using Cmd.Application.Models.User.Queries.GetAllDegree;
using Cmd.Application.Models.User.Queries.Inquire;
using Cms.Endpoints.Site.Atteribute;
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

        [HttpPost("Inquire")]
        public async Task<IActionResult> Inquire(InquireUserQuery query)
        {
            var result = await _sender.Send(query);

            if (result.ResponseCode == 400)
            {
                Response.StatusCode = 400;

                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = _sender.Send(command);

            if (result.Result.ResponseCode == 400)
            {
                Response.StatusCode = 400;

                return BadRequest(result.Result.Message);
            }

            return Ok(result.Result);
        }

        [HttpPost("LoginPassword")]
        public async Task<IActionResult> LoginPassword(LoginCommand command)
        {
            var result = await _sender.Send(command);

            if (result.ResponseCode == 400)
            {
                HttpContext.Response.StatusCode = 400;
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("SetPassword")]
        [AuthorizeToken]
        public async Task<IActionResult> SetPassword(SetPasswordCommand command)
        {
            await _sender.Send(command);


            return Ok("پسورد با موفقیت تنظیم شد.");
        }

        [HttpGet("GetAccountInfo")]
        [AuthorizeToken]
        public async Task<IActionResult> GetAccountInfo()
        {
            var query = new GetAccountInfoQuery()
            {
                Token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim()
            };

            var result = await _sender.Send(query);

            return Ok(result);
        }

        [HttpPost("LoginOtp")]
        public async Task<IActionResult> LoginOtp(AuthenticateCommand command)
        {
            var result = await _sender.Send(command);
            if (result.ResponseCode == 400)
            {
                HttpContext.Response.StatusCode = 400;
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var result = await _sender.Send(command);

            return Ok(result);
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail(SendEmailCommand command)
        {
            try
            {
                await _sender.Send(command);
                return Ok("Email Send Successfully...");

            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("GetAllDegree")]
        public async Task<IActionResult> GetAllDegree()
        {
            var result = await _sender.Send(new GetAllDegreeQuery());
            if (!result.QueryResult.Any())
            {
                return NotFound("هیچ مدرک تحصیلی یافت نشد");
            }

            return Ok(result);
        }
    }
}
