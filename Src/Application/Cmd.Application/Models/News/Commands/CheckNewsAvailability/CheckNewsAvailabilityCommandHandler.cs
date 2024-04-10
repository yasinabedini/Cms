using Cmd.Application.Common.Commands;
using Cms.Domain.Models.News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.CheckNewsAvailability
{
    public class CheckNewsAvailabilityCommandHandler : ICommandHandler<CheckNewsAvailabilityCommand, bool>
    {
        private readonly INewsRepository _repository;

        public CheckNewsAvailabilityCommandHandler(INewsRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(CheckNewsAvailabilityCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.CheckAvailability(request.Id));
        }
    }
}
