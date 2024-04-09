using Cmd.Application.Common.Queries;
using Cmd.Application.Models.News.Queries.Common;
using Cms.Domain.Models.News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.GetNewsTypeById
{
    public class GetNewsTypeByIdQueryHandler : IQueryHandler<GetNewsTypeByIdQuery, NewsTypeViewModel>
    {
        private readonly INewsTypeRepository _repository;

        public GetNewsTypeByIdQueryHandler(INewsTypeRepository repository)
        {
            _repository = repository;
        }

        public Task<NewsTypeViewModel> Handle(GetNewsTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var newsType = _repository.GetById(request.Id);

            return Task.FromResult(new NewsTypeViewModel(newsType.Title.Value, newsType.Name.Value, newsType.IsPage));
        }
    }
}
