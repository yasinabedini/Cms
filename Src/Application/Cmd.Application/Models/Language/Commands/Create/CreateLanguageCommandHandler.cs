using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Language.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Language.Commands.Create
{
    public class CreateLanguageCommandHandler : ICommandHandler<CreateLanguageCommand>
    {
        private readonly ILanguageRepository _repository;

        public CreateLanguageCommandHandler(ILanguageRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            _repository.Add(Cms.Domain.Models.Language.Entities.Language.Create(request.Title, request.Name, request.Rtl, request.Region));
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
