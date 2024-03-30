using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Language.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Language.Commands.Delete
{
    public class DeleteLanguageCommandHandler : ICommandHandler<DeleteLanguageCommand>
    {
        private readonly ILanguageRepository _repository;

        public DeleteLanguageCommandHandler(ILanguageRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.Id);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
