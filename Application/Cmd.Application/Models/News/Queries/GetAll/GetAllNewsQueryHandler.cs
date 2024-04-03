using Cmd.Application.Common.Queries;
using Cmd.Application.Models.News.Queries.Common;
using Cms.Domain.Models.News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.GetAll
{
    public class GetAllNewsQueryHandler : IQueryHandler<GetAllNewsQuery, PagedData<NewsViewModel>>
    {
        private readonly INewsRepository _repository;

        public GetAllNewsQueryHandler(INewsRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedData<NewsViewModel>> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
        {
            var newsList = _repository.GetList().Skip(request.SkipCount).Take(request.PageSize).OrderByDescending(t => t.CreateAt);
            if (request.TypeId is not 0 || request.TypeId is not null)
            {
                newsList.Where(t => t.NewsTypeId == request.TypeId);
            }

            return Task.FromResult(new PagedData<NewsViewModel> { QueryResult = newsList.Select(t => new NewsViewModel(t.Title.Value, t.LanguageId, t.NewsTypeId, t.PublishDate, t.FirstParagraph.Value, t.SeconodParagraph.Value, t.ThirdParagraph.Value, t.MainImageName.Value, t.SecondImage.Value, t.ThirdImage.Value)).ToList(), PageNumber = request.PageNumber, PageSize = request.PageSize });
        }
    }
}
