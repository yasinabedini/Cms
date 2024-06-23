using Cmd.Application.Common.Commands;
using Cmd.Application.Tools;
using Cms.Domain.Models.User.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, Tuple<bool, string>>
    {
        private readonly IUserRepository _repository;

        public LoginCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<Tuple<bool, string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = _repository.GetUserByPhoneNumber(request.PhoneNumber);

            string hassPass = HashPassword.HashUsingSHA256(request.Password);

            if (!user.PhoneConfirmed)
            {
                return Task.FromResult(new Tuple<bool, string>(false, "Phone Number Is Not Confirmed ..."));
            }
            else if (user.IsBlocked)
            {
                return Task.FromResult(new Tuple<bool, string>(false, "Account Is Blocked ..."));
            }
            else if (_repository.Login(request.PhoneNumber, hassPass))
            {
                return Task.FromResult(new Tuple<bool, string>(true, "Login was successful ..."));
            }
            else
            {
                return Task.FromResult(new Tuple<bool, string>(false, "Problems have occurred  ..."));
            }
        }
    }
}
