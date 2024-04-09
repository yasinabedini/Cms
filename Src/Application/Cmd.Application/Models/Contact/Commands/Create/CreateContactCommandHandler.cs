using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Contact.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Contact.Commands.Create
{
    public class CreateContactCommandHandler : ICommandHandler<CreateContactCommand>
    {
        private readonly IContactRepository _repository;

        public CreateContactCommandHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            _repository.Add(Cms.Domain.Models.Contact.Entities.Contact.Create(request.Name, request.Email, request.Text));
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
