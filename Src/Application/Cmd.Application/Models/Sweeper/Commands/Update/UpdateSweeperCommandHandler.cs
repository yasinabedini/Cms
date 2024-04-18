using Cmd.Application.Common.Commands;
using Cmd.Application.Tools;
using Cms.Domain.Models.Sweeper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Commands.Update
{
    public class UpdateSweeperCommandHandler : ICommandHandler<UpdateSweeperCommand>
    {
        private readonly ISweeperRepository _repository;

        public UpdateSweeperCommandHandler(ISweeperRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(UpdateSweeperCommand request, CancellationToken cancellationToken)
        {                                  
            var sweeper = Cms.Domain.Models.Sweeper.Entities.Sweeper.Create(request.Title, request.Text, request.Link, request.Image, request.LanguageId);
            sweeper.SetId(request.Id);

            if (request.IsEnable == false) sweeper.Disable();

            _repository.Update(sweeper);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
