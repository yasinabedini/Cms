using Cmd.Application.Common.Queries;
using Cmd.Application.Models.News.Queries.Common;
using Cmd.Application.Models.News.Queries.GetById;
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

namespace Cmd.Application.Models.News.Queries.GetActivity
{
    public class GetActivityQueryHandler : IQueryHandler<GetActivityQuery, NewsViewModel>
    {
        private readonly INewsRepository _repository;
        private readonly INewsTypeRepository _newsTypeRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IGalleryRepository _galleryRepository;
        private readonly IServiceProvider _serviceProvider;

        public GetActivityQueryHandler(INewsRepository repository, INewsTypeRepository newsTypeRepository, IFileRepository fileRepository, IGalleryRepository galleryRepository, IServiceProvider serviceProvider)
        {
            _repository = repository;
            _newsTypeRepository = newsTypeRepository;
            _fileRepository = fileRepository;
            _galleryRepository = galleryRepository;
            _serviceProvider = serviceProvider;
        }

        public Task<NewsViewModel> Handle(GetActivityQuery request, CancellationToken cancellationToken)
        {
            var config = (IConfiguration)_serviceProvider.GetRequiredService(typeof(IConfiguration));

            var activityId = int.Parse(config.GetSection("NewsActivityId").Value);

            var news = _repository.GetAllWithRelations().Where(t => t.LanguageId == request.LanguageId && t.NewsTypeId == activityId).OrderByDescending(t => t.CreateAt).FirstOrDefault();
            if (news is null)
            {
                return Task.FromResult(new NewsViewModel());
            }

            var newsType = _newsTypeRepository.GetById(news.NewsTypeId);

            var newsViewModel = new NewsViewModel(news.Id, news.Title.Value, news.Introduction.Value, news.LanguageId, news.NewsTypeId, news.PublishDate, news.Text, news.MainImageName.Value, news.SecondImage is not null ? news.SecondImage.Value : "", news.ThirdImage is not null ? news.ThirdImage.Value : "", news.IsEnable, news.Author);
            newsViewModel.NewsType = new NewsTypeViewModel(newsType.Id, newsType.Title.Value, newsType.Name.Value, newsType.IsPage, newsType.IsEnable, (int)newsType.LanguageId);

            var galleries = _galleryRepository.GetNewsGalleries(news.Id).Select(g => new Gallery.Queries.Common.GalleryViewModel
            {
                NewsId = g.NewsId,
                Title = g.Title,
                Type = g.Type,
                Files = _fileRepository.GetGalleryFiles(g.Id).Where(t => t.IsEnable).Select(t => new File.Queries.Common.FileViewModel
                {
                    GalleryId = t.GalleryId,
                    Length = t.Length,
                    Name = t.Name,
                    TypeId = t.TypeId,
                    DisplayName = t.DisplayName,
                    Extension = t.Extension
                }).ToList()
            }).ToList();
            if (galleries.Any(t => t.Files.Any()))
            {
                newsViewModel.Galleries = galleries;
            }

            return Task.FromResult(newsViewModel);
        }
    }
}
