using Cmd.Application.Common.Commands;
using Cmd.Application.Models.User.Queries.Common;
using Cmd.Application.Tools;
using Cms.Domain.Models.Token.Entities;
using Cms.Domain.Models.Token.Repositories;
using Cms.Domain.Models.User.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, TokenViewModel>
    {
        private readonly IUserRepository _repository;
        private readonly ITokenRepository _tokenRepository;

        public LoginCommandHandler(IUserRepository repository, ITokenRepository tokenRepository)
        {
            _repository = repository;
            _tokenRepository = tokenRepository;
        }

        public Task<TokenViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!_repository.PhoneVerified(request.PhoneNumber))
            {
                return Task.FromResult(new TokenViewModel
                {
                    Message = "شماره موبایل را به درستی وارد کنید.",
                    ResponseCode = 400
                });
            }

            var user = _repository.GetUserByPhoneNumber(request.PhoneNumber);

            string hassPass = HashPassword.HashUsingSHA256(request.Password);

            if (_repository.Login(request.PhoneNumber, hassPass))
            {
                var token = _tokenRepository.GenerateToken(user.PhoneNumber.Value);

                _repository.AddUserRefreshTokens(new UserRefreshToken(user.PhoneNumber.Value, token.RefreshToken));

                return Task.FromResult(new TokenViewModel
                {
                    AccessToken = token.AccessToken,
                    ExpiresIn = token.ExpiresIn,
                    RefreshToken = token.RefreshToken,
                    TokenType = token.TokenType,
                    Message="عملیات با موفقیت انجام شد.",
                    ResponseCode = 200
                });
            }
            if (user.IsBlocked)
            {
                return Task.FromResult(new TokenViewModel
                {             
                    Message = "حساب کاربری شما اجازه ورود ندارد!",
                    ResponseCode = 400
                });
            }

            return Task.FromResult(new TokenViewModel
            {
                Message = "شماره موبایل یا رمز عبور به درستی وارد نشده است!",
                ResponseCode = 400
            });
        }
    }
}
