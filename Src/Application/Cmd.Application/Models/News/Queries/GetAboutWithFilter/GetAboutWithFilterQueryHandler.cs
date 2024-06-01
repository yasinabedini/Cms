using Cmd.Application.Common.Queries;
using Cmd.Application.Convertors;
using Cmd.Application.Models.News.Queries.Common;
using Cmd.Application.Models.News.Queries.GetAboutMuseum;
using Cms.Domain.Models.Gallery.Repositories;
using Cms.Domain.Models.News.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.GetAboutWithFilter
{
    public class GetAboutWithFilterQueryHandler : IQueryHandler<GetAboutWithFilterQuery, List<AboutMuseumViewModel>>
    {
        private readonly INewsRepository _repository;
        private readonly INewsTypeRepository _typeRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IGalleryRepository _galleryRepository;

        public GetAboutWithFilterQueryHandler(INewsRepository repository, IServiceProvider serviceProvider, INewsTypeRepository typeRepository, IGalleryRepository galleryRepository)
        {
            _repository = repository;
            _serviceProvider = serviceProvider;
            _typeRepository = typeRepository;
            _galleryRepository = galleryRepository;
        }

        public Task<List<AboutMuseumViewModel>> Handle(GetAboutWithFilterQuery request, CancellationToken cancellationToken)
        {
            var allNews = _repository.GetAllWithRelations().Where(t => t.LanguageId == request.LanguageId).OrderByDescending(t => t.CreateAt).ToList();

            var config = (IConfiguration)_serviceProvider.GetRequiredService(typeof(IConfiguration));


            List<AboutMuseumViewModel> aboutList = new List<AboutMuseumViewModel>();

            var itemsId = config.GetSection("AboutUs").GetChildren();
            foreach (var item in itemsId.Select(t => t.Value))
            {
                long id = long.Parse(item);
                var about = allNews.FirstOrDefault(t => t.NewsTypeId == id);

                if (_typeRepository.GetById(id).LanguageId == request.LanguageId)
                {
                    if (about is not null && about.IsEnable)
                    {
                        var aboutGalleries = _galleryRepository.GetNewsGalleries(about.Id);                     

                        var newsModel  = new NewsViewModel
                        {
                            Id = about.Id,
                            Title = about.Title.Value,
                            Introduction = about.Introduction.Value,
                            LanguageId = about.LanguageId,
                            NewsTypeId = about.NewsTypeId,
                            PublishDate = DateTime.Parse(about.PublishDate).ToShamsi(),
                            Text = about.Text,
                            MainImageName = about.MainImageName.Value,
                            SecondImage = about.SecondImage?.Value,
                            ThirdImage = about.ThirdImage?.Value,
                            Author = about.Author,
                            NewsType = new NewsTypeViewModel(about.NewsType.Id, about.NewsType.Title.Value, about.NewsType.Name.Value, about.NewsType.IsPage, about.NewsType.IsEnable, (int)about.NewsType.LanguageId),
                            IsEnable = about.IsEnable
                        };

                        var model = new AboutMuseumViewModel
                        {
                            TypeId = (int)id,
                            TypeTitle = allNews.FirstOrDefault(t => t.NewsTypeId == id).NewsType.Title.Value,
                            NewsViewModel = newsModel
                        };

                        var galleries = aboutGalleries.Select(t => new Gallery.Queries.Common.GalleryViewModel
                        {
                            NewsId = t.NewsId,
                            Title = t.Title,
                            Type = t.Type,
                            Files = t.Files.Where(t => t.IsEnable).Select(f => new File.Queries.Common.FileViewModel(f.Name, f.GalleryId, f.Length, f.Extension, f.TypeId, f.DisplayName)).ToList()
                        }).ToList();
                        if (galleries.Any(t=>t.Files.Any()))
                        {
                            newsModel.Galleries = galleries;
                        }
                        else
                        {
                            newsModel.Galleries = new List<Gallery.Queries.Common.GalleryViewModel>();
                        }

                        model.NewsViewModel = newsModel;

                        aboutList.Add(model);
                    }
                }
            }
            return Task.FromResult(aboutList);
        }
    }
}
