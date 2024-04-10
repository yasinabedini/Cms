using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.CheckInfoAvailability
{
    public class CheckInfoAvailabilityCommandHandler : ICommandHandler<CheckInfoAvailabilityCommand, bool>
    {
        private readonly IInfoRepository _repository;

        public CheckInfoAvailabilityCommandHandler(IInfoRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(CheckInfoAvailabilityCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.CheckAvailability(request.Id));
        }
    }
}
