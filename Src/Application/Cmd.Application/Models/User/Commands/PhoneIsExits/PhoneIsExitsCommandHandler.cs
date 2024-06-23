using Cmd.Application.Common.Commands;
using Cms.Domain.Models.User.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.PhoneIsExits
{
    public class PhoneIsExitsCommandHandler : ICommandHandler<PhoneIsExitsCommand, bool>
    {
        private readonly IUserRepository _repository;

        public PhoneIsExitsCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(PhoneIsExitsCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.PhoneNumberIsExits(request.PhoneNumber));
        }
    }
}
