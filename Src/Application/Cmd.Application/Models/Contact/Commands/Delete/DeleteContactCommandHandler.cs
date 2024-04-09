using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Contact.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Contact.Commands.Delete
{
    public class DeleteContactCommandHandler : ICommandHandler<DeleteContactCommand>
    {
        private readonly IContactRepository _repository;

        public DeleteContactCommandHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.Id);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
