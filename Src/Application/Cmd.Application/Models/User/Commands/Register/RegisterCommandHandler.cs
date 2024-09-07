using Cmd.Application.Common.Commands;
using Cmd.Application.Models.User.Queries.Common;
using Cmd.Application.Tools;
using Cms.Domain.Models.Sms.Repositories;
using Cms.Domain.Models.Token.Entities;
using Cms.Domain.Models.Token.Repositories;
using Cms.Domain.Models.User.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Cmd.Application.Models.User.Commands.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, TokenViewModel>
    {
        private readonly IUserRepository _repository;
        private readonly ISmsRepository _smsRepository;
        private readonly ITokenRepository _tokenRepository;

        public RegisterCommandHandler(IUserRepository repository, ISmsRepository smsRepository, ITokenRepository tokenRepository)
        {
            _repository = repository;
            _smsRepository = smsRepository;
            _tokenRepository = tokenRepository;
        }

        public Task<TokenViewModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
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

            if (user is not null) return Task.FromResult(new TokenViewModel { ResponseCode = 400,Message="ثبت نام با این شماره موبایل امکان پذیر نیست!" });

            if (!_smsRepository.CheckCode(request.PhoneNumber, request.Otp)) return Task.FromResult(new TokenViewModel { ResponseCode = 400, Message = "کد تایید وارد شده معتبر نیست!" });

            var newUser = Cms.Domain.Models.User.Entities.User.Create(request?.FirstName, request?.LastName, request.PhoneNumber, request?.Email,request?.DegreeId,request?.Study);

            _repository.Add(newUser);
            _repository.Save();

            var token = _tokenRepository.GenerateToken(request.PhoneNumber);

            _repository.AddUserRefreshTokens(new UserRefreshToken(request.PhoneNumber, token.RefreshToken));

            return Task.FromResult(new TokenViewModel
            {
                AccessToken = token.AccessToken,
                ExpiresIn = token.ExpiresIn,
                RefreshToken = token.RefreshToken,
                TokenType = token.TokenType,
                Message="عملیات با موفقیت انجام شد",
                ResponseCode = 200
            });
        }
    }
}
