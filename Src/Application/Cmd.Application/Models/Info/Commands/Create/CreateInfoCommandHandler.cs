using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Info.Repositories;
using Cms.Domain.Models.Language.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.Create
{
    public class CreateInfoCommandHandler : ICommandHandler<CreateInfoCommand>
    {
        private readonly IInfoRepository _repository;
        private readonly ILanguageRepository _languageRepository;

        public CreateInfoCommandHandler(IInfoRepository repository, ILanguageRepository languageRepository)
        {
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public Task Handle(CreateInfoCommand request, CancellationToken cancellationToken)
        {
            if (_languageRepository.GetById(request.LanguageId) is null)
            {
                Task.FromException(new Exception("Language id is not available."));
            }


            _repository.Add(Cms.Domain.Models.Info.Entities.Info.Create(request.Address, request.WorkTime, request.PhoneNumber, request.EmailAddress, request.InstagramAddress, request.LanguageId));
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
