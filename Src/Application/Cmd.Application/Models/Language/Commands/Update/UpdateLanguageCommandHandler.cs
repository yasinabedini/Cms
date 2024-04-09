using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Language.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Language.Commands.Update
{
    public class UpdateLanguageCommandHandler : ICommandHandler<UpdateLanguageCommand>
    {
        private readonly ILanguageRepository _repository;

        public UpdateLanguageCommandHandler(ILanguageRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = Cms.Domain.Models.Language.Entities.Language.Create(request.Title, request.Name, request.Rtl, request.Region);
            language.SetId(request.Id);

            _repository.Update(language);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
