using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.DeleteLink
{
    public class DeleteLinkCommandHandler : ICommandHandler<DeleteLinkCommand>
    {
        private readonly IInfoLinkRepository _repository;

        public DeleteLinkCommandHandler(IInfoLinkRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(DeleteLinkCommand request, CancellationToken cancellationToken)
        {
            var model = _repository.GetById(request.Id);
            model.IsDelete = true;
            _repository.Update(model);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
