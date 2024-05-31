using Cmd.Application.Common.Queries;
using Cmd.Application.Models.News.Queries.Common;
using Cms.Domain.Models.News.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cmd.Application.Models.News.Queries.GetAboutMuseum
{
    public class GetAboutMuseumQueryHandler : IQueryHandler<GetAboutMuseumQuery, List<AboutMuseumViewModel>>
    {
        private readonly INewsRepository _repository;        
        private readonly INewsTypeRepository _typeRepository;
        private readonly IServiceProvider _serviceProvider;

        public GetAboutMuseumQueryHandler(INewsRepository repository, IServiceProvider serviceProvider, INewsTypeRepository typeRepository)
        {
            _repository = repository;
            _serviceProvider = serviceProvider;
            _typeRepository = typeRepository;
        }

        public Task<List<AboutMuseumViewModel>> Handle(GetAboutMuseumQuery request ,CancellationToken cancellationToken)
        {
            var allNews = _repository.GetAllWithRelations().OrderByDescending(t => t.CreateAt).Where(t=>t.LanguageId==request.LanguageId).ToList();

            var config = (IConfiguration)_serviceProvider.GetRequiredService(typeof(IConfiguration));


            List<AboutMuseumViewModel> aboutList = new List<AboutMuseumViewModel>();

            var itemsId = config.GetSection("AboutUs").GetChildren();
            foreach (var item in itemsId.Select(t=>t.Value))
            {
                long id = long.Parse(item);
                var about = allNews.FirstOrDefault(t => t.NewsTypeId == id);

                if (_typeRepository.GetById(id).LanguageId == request.LanguageId)
                {
                    if (about is not null)
                    {
                        aboutList.Add(new AboutMuseumViewModel
                        {
                            TypeId = (int)id,
                            TypeTitle = allNews.FirstOrDefault(t => t.NewsTypeId == id).NewsType.Title.Value,
                            NewsViewModel = new NewsViewModel(about.Id, about.Title.Value, about.Introduction.Value, about.LanguageId, about.NewsTypeId, about.PublishDate, about.Text, about.MainImageName.Value, about.SecondImage?.Value, about.ThirdImage?.Value, about.IsEnable, about.ThumbNailImage.Value, about.Author)
                        });
                    }
                    else
                    {
                        aboutList.Add(new AboutMuseumViewModel
                        {
                            TypeId = (int)id,
                            TypeTitle = _typeRepository.GetById(long.Parse(item)).Title.Value,
                            NewsViewModel = new NewsViewModel()
                        });
                    }
                }
            }            
            return Task.FromResult(aboutList);
        }
    }
}
