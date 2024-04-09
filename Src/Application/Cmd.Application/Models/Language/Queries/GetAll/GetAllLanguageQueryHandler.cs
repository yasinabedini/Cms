using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Language.Queries.Common;
using Cms.Domain.Models.Language.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Language.Queries.GetAll
{
    public class GetAllLanguageQueryHandler : IQueryHandler<GetAllLanguageQuery, PagedData<LanguageViewModel>>
    {
        private readonly ILanguageRepository _repository;

        public GetAllLanguageQueryHandler(ILanguageRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedData<LanguageViewModel>> Handle(GetAllLanguageQuery request, CancellationToken cancellationToken)
        {
            var languages = _repository.GetList().Skip(request.SkipCount).Take(request.PageSize).OrderByDescending(t=>t.CreateAt);

            return Task.FromResult(new PagedData<LanguageViewModel> { QueryResult = languages.Select(t => new LanguageViewModel(t.Title, t.Name, t.Rtl, t.Region)).ToList(),PageSize = request.PageSize,PageNumber = request.PageNumber});
        }
    }
}
