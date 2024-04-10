using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Language.Repositories;
using Cms.Infra.Models.Language.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Language.Commands.CheckLanguageAvailability
{
    public class CheckLanguageAvailabilityCommandHandler : ICommandHandler<CheckLanguageAvailabilityCommand, bool>
    {
        private readonly ILanguageRepository _repository;

        public CheckLanguageAvailabilityCommandHandler(ILanguageRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(CheckLanguageAvailabilityCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.CheckAvailability(request.Id));
        }
    }
}
