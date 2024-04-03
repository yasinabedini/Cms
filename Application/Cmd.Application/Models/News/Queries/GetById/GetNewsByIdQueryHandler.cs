using Cmd.Application.Common.Queries;
using Cmd.Application.Models.News.Queries.Common;
using Cms.Domain.Models.News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.GetById
{
    public class GetNewsByIdQueryHandler : IQueryHandler<GetNewsByIdQuery, NewsViewModel>
    {
        private readonly INewsRepository _repository;

        public GetNewsByIdQueryHandler(INewsRepository repository)
        {
            _repository = repository;
        }

        public Task<NewsViewModel> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        {
            var news = _repository.GetById(request.Id);

            return Task.FromResult(new NewsViewModel(news.Title.Value,news.LanguageId,news.NewsTypeId,news.PublishDate,news.FirstParagraph.Value,news.SeconodParagraph.Value,news.ThirdParagraph.Value,news.MainImageName.Value,news.SeconodParagraph.Value,news.ThirdImage.Value));
        }
    }
}
