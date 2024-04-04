using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Contact.Queries.Common;
using Cms.Domain.Models.Contact.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Contact.Queries.GetById
{
    public class GetContactByIdQueryHandler : IQueryHandler<GetContactByIdQuery, ContactViewModel>
    {
        private readonly IContactRepository _repository;

        public GetContactByIdQueryHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public Task<ContactViewModel> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            var contact = _repository.GetById(request.Id);

            return Task.FromResult(new ContactViewModel(contact.Name, contact.Email, contact.Text.Value));
        }
    }
}
