using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Token.Entities;
using Cms.Domain.Models.Token.Repositories;
using Cms.Domain.Models.User.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, Token>
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;

        public RefreshTokenCommandHandler(ITokenRepository tokenRepository, IUserRepository userRepository)
        {
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
        }

        public Task<Token> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _tokenRepository.GetPrincipalFromExpiredToken(request.AccessToken);
            var phoneNumber = principal.Identity?.Name;

            var savedRefreshToken = _userRepository.GetSavedRefreshTokens(phoneNumber, request.RefreshToken);



            if (savedRefreshToken is null || savedRefreshToken.RefreshToken != request.RefreshToken)
            {
                return Task.FromResult(new Token("","",0,""));
            }

            var newJwtToken = _tokenRepository.GenerateRefreshToken(phoneNumber);

            if (newJwtToken == null)
            {
                return Task.FromResult(new Token("", "", 0, ""));
            }

            UserRefreshToken obj = new UserRefreshToken(phoneNumber, newJwtToken.RefreshToken);
            

            _userRepository.DeleteUserRefreshTokens(phoneNumber, request.RefreshToken);
            _userRepository.AddUserRefreshTokens(obj);

            return Task.FromResult(newJwtToken);
        }
    }
}
