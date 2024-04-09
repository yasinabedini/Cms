using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Sweeper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Commands.Delete
{
    public class DeleteSweeperCommandHandler : ICommandHandler<DeleteSweeperCommand>
    {
        private readonly ISweeperRepository _repository;

        public DeleteSweeperCommandHandler(ISweeperRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(DeleteSweeperCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.Id);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
