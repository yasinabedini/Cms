using Cmd.Application.Common.Commands;
using Cms.Domain.Models.User.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.ConfirmPhone
{
    public class ConfirmPhoneCommandHandler : ICommandHandler<ConfirmPhoneCommand, bool>
    {
        private readonly IUserRepository _repository;

        public ConfirmPhoneCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(ConfirmPhoneCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.ConfirmPhoneNumber(request.PhoneNumber, request.Code));
        }
    }
}
