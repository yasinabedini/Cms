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
            var language = _repository.GetById(request.Id);

            language.ChangeName(request.Name);
            language.ChangeRegion(request.Region);
            language.ChangeTitle(request.Title);
            language.ChangeRtl(request.Rtl);
            language.ChangeIsEnable(request.IsEnable);

            _repository.Update(language);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
