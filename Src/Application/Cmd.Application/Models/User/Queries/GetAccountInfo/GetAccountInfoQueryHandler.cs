using Cmd.Application.Common.Queries;
using Cmd.Application.Models.User.Queries.Common;
using Cms.Domain.Models.Token.Repositories;
using Cms.Domain.Models.User.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Queries.GetAccountInfo
{
    public class GetAccountInfoQueryHandler : IQueryHandler<GetAccountInfoQuery, UserViewModel>
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        public GetAccountInfoQueryHandler(ITokenRepository tokenRepository, IUserRepository userRepository)
        {
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
        }

        public Task<UserViewModel> Handle(GetAccountInfoQuery request, CancellationToken cancellationToken)
        {
            var phoneNumber = _tokenRepository.GetPrincipalFromExpiredToken(request.Token).Identity.Name;

            var user = _userRepository.GetUserByPhoneNumber(phoneNumber);

            return Task.FromResult(new UserViewModel
            {
                FirstName = user.FirstName.Value,
                LastName = user.LastName.Value,
                PhoneNumber = phoneNumber,
                PhoneConfirmed = user.PhoneConfirmed,
                LastLoginDate = user.LastLoginDate,
                IsBlocked = user.IsBlocked
            });
        }
    }
}