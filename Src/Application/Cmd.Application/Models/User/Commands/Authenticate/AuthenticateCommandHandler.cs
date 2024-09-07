using Cmd.Application.Common.Commands;
using Cmd.Application.Models.User.Queries.Common;
using Cmd.Application.Security.Auth;
using Cms.Domain.Models.Sms.Repositories;
using Cms.Domain.Models.Token.Entities;
using Cms.Domain.Models.Token.Repositories;
using Cms.Domain.Models.User.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.Authenticate
{
    public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, TokenViewModel>
    {
        private readonly JwtOptions _appSettings;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _repository;
        private readonly ISmsRepository _smsRepository;

        public AuthenticateCommandHandler(IOptions<JwtOptions> appSettings, IUserRepository repository, ITokenRepository tokenRepository, ISmsRepository smsRepository)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
            _tokenRepository = tokenRepository;
            _smsRepository = smsRepository;
        }

        public async Task<TokenViewModel> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            if (!_repository.PhoneVerified(request.Mobile))
            {
                return await Task.FromResult(new TokenViewModel
                {
                    Message = "شماره موبایل را به درستی وارد کنید.",
                    ResponseCode = 400
                });
            }

            var user = _repository.GetUserByPhoneNumber(request.Mobile);

            // return null if user not found
            if (user is null) return await Task.FromResult(new TokenViewModel { Message = "ورود با این شماره موبایل امکان پذیر نیست!", ResponseCode = 400 });

            else if (!_smsRepository.CheckCode(request.Mobile, request.Code)) return await Task.FromResult(new TokenViewModel { Message = "کد تایید وارد شده معتبر نیست!", ResponseCode = 400 });

            // authentication successful so generate jwt token
            var token = _tokenRepository.GenerateToken(user.PhoneNumber.Value);

            _repository.AddUserRefreshTokens(new UserRefreshToken(user.PhoneNumber.Value, token.RefreshToken));

            return await Task.FromResult(new TokenViewModel
            {
                AccessToken = token.AccessToken,
                ExpiresIn = token.ExpiresIn,
                RefreshToken = token.RefreshToken,
                TokenType = token.TokenType,
                Message = "عملیات با موفقیت انجام شد.",
                ResponseCode = 200
            });
        }
    }
}
