using Cmd.Application.Common.Queries;
using Cmd.Application.Models.News.Queries.Common;
using Cms.Domain.Models.File.Repositories;
using Cms.Domain.Models.Gallery.Repositories;
using Cms.Domain.Models.News.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.GetAllActivity
{
    public class GetAllActivityQueryHandler : IQueryHandler<GetAllActivityQuery, PagedData<NewsViewModel>>
    {
        private readonly INewsRepository _repository;
        private readonly INewsTypeRepository _newsTypeRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IGalleryRepository _galleryRepository;
        private readonly IServiceProvider _serviceProvider;

        public GetAllActivityQueryHandler(INewsRepository repository, INewsTypeRepository newsTypeRepository, IFileRepository fileRepository, IGalleryRepository galleryRepository, IServiceProvider serviceProvider)
        {
            _repository = repository;
            _newsTypeRepository = newsTypeRepository;
            _fileRepository = fileRepository;
            _galleryRepository = galleryRepository;
            _serviceProvider = serviceProvider;
        }

        public Task<PagedData<NewsViewModel>> Handle(GetAllActivityQuery request, CancellationToken cancellationToken)
        {

            var config = (IConfiguration)_serviceProvider.GetRequiredService(typeof(IConfiguration));

            var activityId = int.Parse(config.GetSection("NewsActivityId").Value);

            var news = _repository.GetAllWithRelations().Where(t => t.LanguageId == request.LanguageId && t.NewsTypeId == activityId).OrderByDescending(t => t.CreateAt);
            if (news is null)
            {
                return Task.FromResult(new PagedData<NewsViewModel>());
            }

            var newsType = _newsTypeRepository.GetById(activityId);

            var newsModel = news.Select(news => new NewsViewModel(news.Id, news.Title.Value, news.Introduction.Value, news.LanguageId, news.NewsTypeId, news.PublishDate, news.Text, news.MainImageName.Value, news.SecondImage is not null ? news.SecondImage.Value : "", news.ThirdImage is not null ? news.ThirdImage.Value : "", news.IsEnable, news.ThumbNailImage.Value, news.Author));
            newsModel.ToList().ForEach(t => t.NewsType = new NewsTypeViewModel(newsType.Id, newsType.Title.Value, newsType.Name.Value, newsType.IsPage, newsType.IsEnable, (int)newsType.LanguageId));
            

            return Task.FromResult(new PagedData<NewsViewModel>
            {
                QueryResult = newsModel.ToList(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = newsModel.Count()
            });
        }
    }
}
