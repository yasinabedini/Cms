using Cmd.Application.Common.Commands;
using Cmd.Application.Security.Auth;
using Cms.Domain.Models.User.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.Authenticate
{
    public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, SignInResult>
    {
        private readonly JwtOptions _appSettings;
        private readonly IUserRepository _repository;

        public AuthenticateCommandHandler(IOptions<JwtOptions> appSettings, IUserRepository repository)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public async Task<SignInResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = _repository.GetUserByPhoneNumber(request.Mobile);

            // return null if user not found
            if (user is null) return null;
            else if (!user.CheckVerificationCode(request.Code)) return null;

            // authentication successful so generate jwt token
            var token = await generateJwtToken(user.PhoneNumber.Value);

            return await Task.FromResult(new SignInResult("Bearer",token, 86400, ""));
        }

        private async Task<string> generateJwtToken(string phone)
        {
            //Generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {

                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("phoneNumber", phone) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });

            return tokenHandler.WriteToken(token);
        }
    }
}
