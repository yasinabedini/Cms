using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Info.Entities;
using Cms.Domain.Models.Info.Repositories;
using Cms.Domain.Models.Language.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.Update
{
    public class UpdateInfoCommandHandler : ICommandHandler<UpdateInfoCommand>
    {
        private readonly IInfoRepository _repository;
        private readonly ILanguageRepository _languageRepository;

        public UpdateInfoCommandHandler(IInfoRepository repository, ILanguageRepository languageRepository)
        {
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public Task Handle(UpdateInfoCommand request, CancellationToken cancellationToken)
        {
            var info = Cms.Domain.Models.Info.Entities.Info.Create(request.Address, request.WorkTime, request.PhoneNumber, request.EmailAddress, request.InstagramAddress, request.LanguageId);
            info.SetId(request.Id);

            if (_languageRepository.GetById(request.LanguageId) is null)
            {
                Task.FromException(new Exception("Language id is not available."));
            }

            _repository.Update(info);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
