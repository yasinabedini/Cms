using Cmd.Application.Common.Commands;
using Cms.Domain.Models.News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.CheckNewsTypeAvailability
{
    public class CheckNewsTypeAvailabilityCommandHandler : ICommandHandler<CheckNewsTypeAvailabilityCommand, bool>
    {
        private readonly INewsTypeRepository _repository;

        public CheckNewsTypeAvailabilityCommandHandler(INewsTypeRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(CheckNewsTypeAvailabilityCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.CheckAvailability(request.Id));
        }
    }
}
