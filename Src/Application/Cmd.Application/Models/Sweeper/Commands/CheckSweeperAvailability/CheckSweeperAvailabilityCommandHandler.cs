using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Sweeper.Repositories;
using Cms.Infra.Models.Sweeper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Commands.CheckSweeperAvailability
{
    public class CheckSweeperAvailabilityCommandHandler : ICommandHandler<CheckSweeperAvailabilityCommand, bool>
    {
        private readonly ISweeperRepository _repository;

        public CheckSweeperAvailabilityCommandHandler(ISweeperRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(CheckSweeperAvailabilityCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.CheckAvailability(request.Id));
        }
    }
}
