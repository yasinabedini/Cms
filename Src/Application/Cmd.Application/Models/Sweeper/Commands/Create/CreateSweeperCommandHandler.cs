using Cmd.Application.Common.Commands;
using Cmd.Application.Tools;
using Cms.Domain.Models.Language.Repositories;
using Cms.Domain.Models.Sweeper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Commands.Create
{
    public class CreateSweeperCommandHandler : ICommandHandler<CreateSweeperCommand>
    {
        private readonly ISweeperRepository _repository;
        private readonly ILanguageRepository _languageRepository;
        public CreateSweeperCommandHandler(ISweeperRepository repository, ILanguageRepository languageRepository)
        {
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public Task Handle(CreateSweeperCommand request, CancellationToken cancellationToken)
        {                        
            _repository.Add(Cms.Domain.Models.Sweeper.Entities.Sweeper.Create(request.Title, request.Text, request.Link, request.Image, request.LanguageId));
            _repository.Save();
            if (_languageRepository.GetById(request.LanguageId) is null)
            {
                return Task.FromException(new Exception("Language Id Is Not Available."));
            }

            return Task.CompletedTask;
        }
    }
}
