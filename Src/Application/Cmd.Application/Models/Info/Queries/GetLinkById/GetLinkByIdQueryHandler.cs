using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Info.Queries.Common;
using Cms.Domain.Models.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Queries.GetLinkById
{
    public class GetLinkByIdQueryHandler : IQueryHandler<GetLinkByIdQuery, InfoLinkViewModel>
    {
        private readonly IInfoLinkRepository _repository;

        public GetLinkByIdQueryHandler(IInfoLinkRepository repository)
        {
            _repository = repository;
        }

        public Task<InfoLinkViewModel> Handle(GetLinkByIdQuery request, CancellationToken cancellationToken)
        {
            var model = _repository.GetById(request.Id);
            if (model is null) return Task.FromResult(new InfoLinkViewModel());
            return Task.FromResult(new InfoLinkViewModel(model.Id,model.Title,model.Link,model.LanguageId));
        }
    }
}
