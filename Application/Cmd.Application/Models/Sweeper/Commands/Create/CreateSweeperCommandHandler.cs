using Cmd.Application.Common.Commands;
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

        public CreateSweeperCommandHandler(ISweeperRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(CreateSweeperCommand request, CancellationToken cancellationToken)
        {
            _repository.Add(Cms.Domain.Models.Sweeper.Entities.Sweeper.Create(request.Title, request.Text, request.Link, request.ImageName, request.LanguageId));
            _repository.Save();

           return Task.CompletedTask;
        }
    }
}
