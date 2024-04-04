using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Contact.Queries.Common;
using Cms.Domain.Models.Contact.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Contact.Queries.GetAll
{
    public class GetAllContactQueryHandler : IQueryHandler<GetAllContactQuery, PagedData<ContactViewModel>>
    {
        private readonly IContactRepository _repository;

        public GetAllContactQueryHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedData<ContactViewModel>> Handle(GetAllContactQuery request, CancellationToken cancellationToken)
        {
            var contacts = _repository.GetList().Skip(request.SkipCount).Take(request.PageSize);

            return Task.FromResult(new PagedData<ContactViewModel> { QueryResult = contacts.Select(t => new ContactViewModel(t.Name, t.Email, t.Text.Value)).ToList(), PageNumber = request.PageNumber, PageSize = request.PageSize });
        }
    }
}
