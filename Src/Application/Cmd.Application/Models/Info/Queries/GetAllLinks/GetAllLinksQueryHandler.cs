using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Info.Queries.Common;
using Cms.Domain.Models.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Queries.GetAllLinks
{
    public class GetAllLinksQueryHandler : IQueryHandler<GetAllLinksQuery, List<InfoLinkViewModel>>
    {
        private readonly IInfoLinkRepository _repository;

        public GetAllLinksQueryHandler(IInfoLinkRepository repository)
        {
            _repository = repository;
        }

        public Task<List<InfoLinkViewModel>> Handle(GetAllLinksQuery request, CancellationToken cancellationToken)
        {
            var link = _repository.GetList();
            var links = _repository.GetList().OrderByDescending(t=>t.CreateAt).Select(t => new InfoLinkViewModel(t.Id, t.Title, t.Link, t.LanguageId)).ToList();
            if (request.LanguageId != 0)
            {
                links = links.Where(t => t.LanguageId == request.LanguageId).ToList();
            }
            return Task.FromResult(links);
        }
    }
}
