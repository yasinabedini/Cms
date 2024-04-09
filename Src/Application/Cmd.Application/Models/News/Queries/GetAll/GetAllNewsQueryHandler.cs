using Cmd.Application.Common.Queries;
using Cmd.Application.Models.News.Queries.Common;
using Cms.Domain.Models.News.Repository;

namespace Cmd.Application.Models.News.Queries.GetAll;

public class GetAllNewsQueryHandler : IQueryHandler<GetAllNewsQuery, PagedData<NewsViewModel>>
{
    private readonly INewsRepository _repository;

    public GetAllNewsQueryHandler(INewsRepository repository)
    {
        _repository = repository;
    }

    public Task<PagedData<NewsViewModel>> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
    {
        var newsList = _repository.GetAllWithRelations().Skip(request.SkipCount).Take(request.PageSize).OrderByDescending(t => t.CreateAt).Where(t => t.NewsType.IsPage = request.IsPage);

        if (request.TypeId is not 0 || request.TypeId is not null)
        {
            newsList.Where(t => t.NewsTypeId == request.TypeId);
        }


        if (request.LanguageId is not 0 || request.LanguageId is not null)
        {
            newsList.Where(t => t.LanguageId == request.LanguageId);
        }

        return Task.FromResult(
            new PagedData<NewsViewModel>
            {
                QueryResult = newsList.Select(t => new NewsViewModel(
                    t.Title.Value,
                    t.Introduction.Value,
                    t.Text,
                    t.LanguageId,
                    t.NewsTypeId,
                    t.PublishDate,
                    t.MainImageName.Value,
                    t.SecondImage is not null ? t.SecondImage.Value : "",
                    t.ThirdImage is not null ? t.ThirdImage.Value : "")).ToList(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            });
    }
}
