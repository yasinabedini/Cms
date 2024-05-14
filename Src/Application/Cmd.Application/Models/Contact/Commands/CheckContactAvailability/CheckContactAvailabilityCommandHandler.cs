using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Contact.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Contact.Commands.CheckContactAvailability
{
    public class CheckContactAvailabilityCommandHandler : ICommandHandler<CheckContactAvailabilityCommand, bool>
    {
        private readonly IContactRepository _repository;


        public CheckContactAvailabilityCommandHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(CheckContactAvailabilityCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.CheckAvailability(request.Id));
        }
    }
}
