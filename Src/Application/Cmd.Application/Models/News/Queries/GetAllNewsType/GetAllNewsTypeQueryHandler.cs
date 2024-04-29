using Cmd.Application.Common.Queries;
using Cmd.Application.Models.News.Queries.Common;
using Cms.Domain.Models.News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.GetAllNewsType
{
    public class GetAllNewsTypeQueryHandler:IQueryHandler<GetAllNewsTypeQuery,PagedData<NewsTypeViewModel>>
    {
        private readonly INewsTypeRepository _repository;

        public GetAllNewsTypeQueryHandler(INewsTypeRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedData<NewsTypeViewModel>> Handle(GetAllNewsTypeQuery request, CancellationToken cancellationToken)
        {
            var newsTypes = _repository.GetList().Skip(request.SkipCount).Take(request.PageSize);

            return Task.FromResult(new PagedData<NewsTypeViewModel> { QueryResult = newsTypes.Select(t => new NewsTypeViewModel(t.Id, t.Title.Value, t.Name.Value,t.IsPage,t.IsEnable, (int)t.LanguageId)).ToList(), PageNumber = request.PageNumber, PageSize = request.PageSize });
        }
    }
}
