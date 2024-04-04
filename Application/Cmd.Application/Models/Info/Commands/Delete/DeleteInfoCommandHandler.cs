using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.Delete
{
    public class DeleteInfoCommandHandler : ICommandHandler<DeleteInfoCommand>
    {
        private readonly IInfoRepository _repository;

        public DeleteInfoCommandHandler(IInfoRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(DeleteInfoCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.Id);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
