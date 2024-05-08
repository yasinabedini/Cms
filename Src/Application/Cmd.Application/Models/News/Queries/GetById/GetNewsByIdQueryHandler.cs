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
        private readonly INewsTypeRepository _newsTypeRepository;

        public GetNewsByIdQueryHandler(INewsRepository repository, INewsTypeRepository newsTypeRepository)
        {
            _repository = repository;
            _newsTypeRepository = newsTypeRepository;
        }

        public Task<NewsViewModel> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        {
            var news = _repository.GetById(request.Id);
            var newsType = _newsTypeRepository.GetById(news.NewsTypeId);

            var newsViewModel = new NewsViewModel(news.Id,news.Title.Value, news.Introduction.Value, news.LanguageId, news.NewsTypeId,  news.PublishDate, news.Text, news.MainImageName.Value, news.SecondImage is not null ? news.SecondImage.Value : "", news.ThirdImage is not null ? news.ThirdImage.Value : "",news.IsEnable, news.Author);
            newsViewModel.NewsType = new NewsTypeViewModel(newsType.Id,newsType.Title.Value, newsType.Name.Value, newsType.IsPage, newsType.IsEnable, (int)newsType.LanguageId);

            return Task.FromResult(newsViewModel);
        }
    }
}
