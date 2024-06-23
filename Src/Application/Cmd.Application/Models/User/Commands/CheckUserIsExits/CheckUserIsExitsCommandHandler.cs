using Cmd.Application.Common.Commands;
using Cms.Domain.Models.User.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.CheckUserIsExits
{
    public class CheckUserIsExitsCommandHandler : ICommandHandler<CheckUserIsExitsCommand, bool>
    {
        private readonly IUserRepository _repository;

        public CheckUserIsExitsCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(CheckUserIsExitsCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.CheckUserIsExits(request.PhoneNumber,request.Password));
        }
    }
}
