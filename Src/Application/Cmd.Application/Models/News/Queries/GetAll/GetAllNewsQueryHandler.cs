using Cmd.Application.Common.Queries;
using Cmd.Application.Models.News.Queries.Common;
using Cms.Domain.Models.News.Entities;
using Cms.Domain.Models.News.Repository;

namespace Cmd.Application.Models.News.Queries.GetAll;

public class GetAllNewsQueryHandler : IQueryHandler<GetAllNewsQuery, PagedData<NewsViewModel>>
{
    private readonly INewsRepository _repository;
    private readonly INewsTypeRepository _newsTypeRepository;

    public GetAllNewsQueryHandler(INewsRepository repository, INewsTypeRepository newsTypeRepository)
    {
        _repository = repository;
        _newsTypeRepository = newsTypeRepository;
    }

    public Task<PagedData<NewsViewModel>> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
    {
        var newsList = _repository.GetAllWithRelations().OrderByDescending(t => t.CreateAt).ToList();

        if (request.TypeId is not null && request.TypeId is not 0)
        {
            newsList = newsList.Where(t => t.NewsTypeId == request.TypeId).ToList();
        }
        if (request.LanguageId is not null && request.LanguageId is not 0)
        {
            newsList = newsList.Where(t => t.LanguageId == request.LanguageId).ToList();
        }        
        newsList = newsList.Where(t => t.NewsType.IsPage == request.IsPage).ToList();

        newsList = newsList.Skip(request.SkipCount).Take(request.PageSize).ToList();


        return Task.FromResult(
            new PagedData<NewsViewModel>
            {
                QueryResult = newsList.Select(t => new NewsViewModel
                {
                    Id = t.Id,
                    Title = t.Title.Value,
                    Introduction = t.Introduction.Value,
                    LanguageId = t.LanguageId,
                    NewsTypeId = t.NewsTypeId,
                    PublishDate = t.PublishDate,
                    Text = t.Text,
                    MainImageName = t.MainImageName.Value,
                    SecondImage = t.SecondImage is not null ? t.SecondImage.Value : "",
                    ThirdImage = t.ThirdImage is not null ? t.ThirdImage.Value : "",
                    IsEnable = t.IsEnable,
                    Author = t.Author,
                    Galleries = new List<Gallery.Queries.Common.GalleryViewModel>()
                }).ToList(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            });
    }
}
