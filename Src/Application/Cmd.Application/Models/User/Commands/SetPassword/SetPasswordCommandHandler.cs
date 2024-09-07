using Cmd.Application.Common.Commands;
using Cmd.Application.Tools;
using Cms.Domain.Models.User.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.SetPassword
{
    public class SetPasswordCommandHandler : ICommandHandler<SetPasswordCommand>
    {
        private readonly IUserRepository _repository;

        public SetPasswordCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = _repository.GetUserByPhoneNumber(request.PhoneNumber);

            var hashPassword = HashPassword.HashUsingSHA256(request.Passwrod);
            user.ChangePassword(hashPassword);

            _repository.Update(user);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
